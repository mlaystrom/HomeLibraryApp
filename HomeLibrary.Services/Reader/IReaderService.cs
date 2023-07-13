using HomeLibrary.Models.Reader;

namespace HomeLibrary.Services.Reader;

public interface IReaderService
{
    Task<bool> RegisterReaderAsync(ReaderRegister model);
    Task<bool> LoginAsync(ReaderLogin model);
    Task LogoutAsync();
  
}