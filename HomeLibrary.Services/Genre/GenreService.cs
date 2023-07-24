using HomeLibrary.Data;
using HomeLibrary.Data.Entities;
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
      //returning the list of GenreList Item objects, which represent all the genres from the Db with properties Id and Genre
        return genre;
    }

    public async Task<bool> CreateGenreAsync(GenreCreate model)
    {

    var entity = new GenreEntity
    {
        Genre = model.Genre
    };
        _context.Genre.Add(entity);
        var numberOfChanges = await _context.SaveChangesAsync();

        return numberOfChanges == 1;
    }

    public async Task<GenreDetail> GetGenreAsync(int id)
    {
        var entity = await _context.Genre.FindAsync(id);

        if (entity is null)
            return new GenreDetail();
            GenreDetail model = new()
        {
            Id = entity.Id,
            Genre = entity.Genre
        };
        return model;
    }

    public async Task<bool> UpdateGenreAsync(GenreUpdate model)
    {
        var entity = await _context.Genre.FindAsync(model.Id);

        if (entity is null)
        return false;

        entity.Genre = model.Genre;

        return await _context.SaveChangesAsync() == 1;
    }

    public async Task<bool> DeleteGenreAsync(int id)
    {
        var entity = await _context.Genre.FindAsync(id);
        if (entity is null)
            return false;
        //using this to check if there are books that are using the genre that wanting to delete
        var activeGenre = await _context.Book.Where(b =>b.GenreId ==id).ToListAsync();
        // won't delete the genre if there are books using it
        if(activeGenre.Any())
        {
            return false;
        }

        _context.Genre.Remove(entity);
        return await _context.SaveChangesAsync() == 1;
    }

    
}