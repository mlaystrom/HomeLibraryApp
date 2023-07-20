using HomeLibrary.Models.Book;

namespace HomeLibrary.Services.Book;

public interface IBookService
{
    Task<IEnumerable<BookListItem>> GetAllBooksAsync();
    Task<bool> CreateBookAsync(BookCreate model);
}