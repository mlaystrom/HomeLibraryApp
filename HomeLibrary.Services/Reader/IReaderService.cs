using HomeLibrary.Models.Reader;

namespace HomeLibrary.Services.Reader;

public interface IReaderService
{
    Task<bool> RegisterReaderAsync(ReaderRegister model);
    Task<bool> LoginAsync(ReaderLogin model);
    Task LogoutAsync();
    Task<IEnumerable<ReaderListItem>> GetAllReadersAsync();
    Task<ReaderDetail?> GetReaderByIdAsync(int id);
    Task<bool> UpdateReaderAsync(ReaderEdit model);
    Task<bool> DeleteReaderByIdAsync(int id);
  
}