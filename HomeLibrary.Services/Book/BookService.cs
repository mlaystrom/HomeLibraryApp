using HomeLibrary.Data;
using HomeLibrary.Data.Entities;
using HomeLibrary.Models.Book;
using Microsoft.EntityFrameworkCore;

namespace HomeLibrary.Services.Book;

public class BookService : IBookService
{
        private HomeLibraryDbContext _context;
        private int _readerId;
        private int _genreId;

    //using this to set up Book service methods
    //the methods communicate with the Db and return formatted C# objects that the controller will use
    public BookService(HomeLibraryDbContext context)
        {
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
            GenreId = _genreId,
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
        .Select(b => new BookListItem()
        {   
            Id = b.Id,
            ReaderId = b.ReaderId,
            Title = b.Title,
            Author = b.Author,
            SeriesNumber = b.SeriesNumber,
            GenreId = b.GenreId
        })
        .ToListAsync();//converting selection into a C# list
        return book;
    }

    
    
}