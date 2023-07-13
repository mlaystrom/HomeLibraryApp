using HomeLibrary.Models.Reader;

namespace HomeLibrary.Services.Reader;

public interface IReaderService
{
    Task<bool> RegisterReaderAsync(ReaderRegister model);
    Task<bool> LoginAsync(ReaderLogin model);
    Task LogoutAsync();
    Task<ReaderDetail?> GetReaderByIdAsync(int readerId);
    Task<bool> UpdateReaderAsync(ReaderEdit model);
    Task<bool> DeleteReaderByIdAsync(int id);
  
}