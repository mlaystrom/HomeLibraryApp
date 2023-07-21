using HomeLibrary.Data;
using HomeLibrary.Models.Genre;
using Microsoft.EntityFrameworkCore;

namespace HomeLibrary.Services.Genre;

public class GenreService : IGenreService
{
    //field being injected through constructor
    private readonly HomeLibraryDbContext _context;

    //using this to set up Genre service methods
    //the methods communicate with the Db and return formatted C# objects that the controller will use
    public GenreService(HomeLibraryDbContext context)
    {
        _context = context;
    }

    public async Task<List<GenreListItem>> GetAllGenresAsync()
    {
        //query Db
        var genre = await _context.Genre
        .Select(b => new GenreListItem
        {
            Id = b.Id,
            Genre = b.Genre
        })
      .ToListAsync();//converting selection into a C# list
        return genre;
    }
}