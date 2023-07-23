using HomeLibrary.Models.Genre;

namespace HomeLibrary.Services.Genre;

public interface IGenreService
{
   Task<List<GenreListItem>> GetAllGenresAsync();
   Task<bool> CreateGenreAsync(GenreCreate model);

   Task<GenreDetail> GetGenreAsync(int id);
   Task<bool> UpdateGenreAsync(GenreUpdate model);
   Task<bool> DeleteGenreAsync(int id);


}