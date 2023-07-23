using HomeLibrary.Data;
using HomeLibrary.Data.Entities;
using HomeLibrary.Models.Book;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HomeLibrary.Services.Book;

public class BookService : IBookService
{
        private HomeLibraryDbContext _context;
        private int _readerId;
        private int _genreId;

    //using this to set up Book service methods
    //the methods communicate with the Db and return formatted C# objects that the controller will use
    public BookService(UserManager<ReaderEntity> userManager, SignInManager<ReaderEntity> signInManager,HomeLibraryDbContext context)
        {
        var user = signInManager.Context.User; //looking at who is signed in within the current context

        var claim = userManager.GetUserId(user); //looking at current user and getting the Id claim
        int.TryParse(claim, out _readerId); //taking that claim and converting from a string to an integer and saving to the field _readerId

        _context = context;
        }

    public async Task<bool> CreateBookAsync(BookCreate model)
    {
        var entity = new BooksEntity
        {
            ReaderId = _readerId,
            Title = model.Title,
            Author = model.Author,
            SeriesNumber = model.SeriesNumber,
            DateFinished = model.DateFinished,
            GenreId = model.GenreId,
            Comment = model.Comment
        };
        //adding the new entity to the WishList table
        _context.Book.Add(entity);
        var numberOfChanges = await _context.SaveChangesAsync();

        return numberOfChanges == 1;
    }

    public async Task<IEnumerable<BookListItem>> GetAllBooksAsync()
    {
        var book = await _context.Book.Include(b => b.Reader).Include(b => b.Genre)
        .Where(b => b.ReaderId == _readerId)
        .Select(b => new BookListItem()
        {   
            Id = b.Id,
            ReaderId = b.ReaderId,
            Title = b.Title,
            Author = b.Author,
            SeriesNumber = b.SeriesNumber,
            GenreId = b.GenreId,
            Genre = b.Genre.Genre //1st is genre entity and 2nd is genre name
        })
        .ToListAsync();//converting selection into a C# list
        return book;
    }

    public async Task<BookDetail> GetBooksByIdAsync(int id)
    {
        // 1st retrieve book with the given Id from Db
        var entity = await _context.Book.FindAsync(id);
        // Returning null or new BookDetail
        if (entity is null)
            return new BookDetail();
        BookDetail model = new()
        {
            Id = entity.Id,
            Title = entity.Title,
            Author = entity.Author,
            SeriesNumber = entity.SeriesNumber,
            GenreId = entity.GenreId,
            Comment = entity.Comment
           
        };
        return model;

    }

    public async Task<bool> UpdateBookAsync(BookUpdate model)
    {
        //declaring variable entity
        //searching for an entity in the Book table of the Db
        //the entity being searched for is an entity with the primary key value matching model.Id
        var entity = await _context.Book.FindAsync(model.Id); 

        if (entity is null)
        return false;

            entity.Id = model.Id;
            entity.Title = model.Title;
            entity.Author = model.Author;
            entity.SeriesNumber = model.SeriesNumber;
            entity.GenreId = model.GenreId;
            entity.Comment = model.Comment;

        return await _context.SaveChangesAsync() ==1;
    }

    public async Task<bool> DeleteBooksByIdAsync(int id)
    {   // checking the Dbset for the book within BooksEntity that matches the given id parameter
        var entity = await _context.Book.FindAsync(id);
        if(entity is null)
            return false;
            
        //telling the Dbset to remove the found entity that was deterned not null
        //Save changes to Db and return a boolean that states one change was made
        _context.Book.Remove(entity);
        return await _context.SaveChangesAsync() ==1;
    
    }
    
    
}