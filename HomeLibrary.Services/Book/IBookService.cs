using HomeLibrary.Models.Book;

namespace HomeLibrary.Services.Book;

public interface IBookService
{
    Task<IEnumerable<BookListItem>> GetAllBooksAsync();
    Task<bool> CreateBookAsync(BookCreate model);

    Task<BookDetail>GetBooksByIdAsync(int id);

    Task<bool> UpdateBookAsync (BookUpdate model);
    Task<bool>DeleteBooksByIdAsync(int id);
}