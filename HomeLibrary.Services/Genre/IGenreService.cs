using HomeLibrary.Models.Genre;

namespace HomeLibrary.Services.Genre;

public interface IGenreService
{
   Task<List<GenreListItem>> GetAllGenresAsync();
}