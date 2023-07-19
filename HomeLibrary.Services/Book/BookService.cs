using HomeLibrary.Data;

namespace HomeLibrary.Services.Book;

public class BookService : IBookService
{
        private HomeLibraryDbContext _context;

    //using this to set up Book service methods
    //the methods communicate with the Db and return formatted C# objects that the controller will use
    public BookService(HomeLibraryDbContext context)
        {
            _context = context;
        }
    
}