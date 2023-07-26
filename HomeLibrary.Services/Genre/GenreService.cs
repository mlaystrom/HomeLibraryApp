using HomeLibrary.Data;
using HomeLibrary.Data.Entities;
using HomeLibrary.Models.Genre;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HomeLibrary.Services.Genre;

public class GenreService : IGenreService
{
    //field being injected through constructor
    private readonly HomeLibraryDbContext _context;
    private int _readerId;

    //using this to set up Genre service methods
    //the methods communicate with the Db and return formatted C# objects that the controller will use
    public GenreService(UserManager<ReaderEntity> userManager, SignInManager<ReaderEntity> signInManager, HomeLibraryDbContext context)
    {
        var user = signInManager.Context.User; //looking at who is signed in within the current context

        var claim = userManager.GetUserId(user); //looking at current user and getting the Id claim
        int.TryParse(claim, out _readerId); //taking that claim and converting from a string to an integer and saving to the field _readerId
        _context = context;
        
    }

    public async Task<List<GenreListItem>> GetAllGenresAsync()//to filter genre by reader, have the field _readerId , this represents the current user
    {
        //query Db
        var genre = await _context.Genre
        .Where(b => b.ReaderId == _readerId) // looks for genres by the current user
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
        Genre = model.Genre,
        ReaderId = _readerId
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