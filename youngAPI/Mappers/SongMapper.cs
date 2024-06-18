using youngAPI.Models;
using youngAPI.Dtos.Song;

namespace youngAPI.Mappers
{
    public static class SongMapper
    {
        public static SongDto ToSongDto(this Song songModel)
        {
            return new SongDto
            {
                Id = songModel.Id,
                Title = songModel.Title,
                Description = songModel.Description,
                Audio = songModel.Audio,
                Image = songModel.Image,
                User = songModel.User.UserName
            };
        }

        public static Song ToSongFromCreateDto(this CreateSongRequestDto songDto)
        {
            return new Song
            {
                Title = songDto.Title,
                Description = songDto.Description,
                Audio = songDto.Audio,
                Image = songDto.Image
            };
        }

    }
}

