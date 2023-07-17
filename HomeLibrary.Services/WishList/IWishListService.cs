using HomeLibrary.Models.WishList;

namespace HomeLibrary.Services.WishList;

public interface IWishListService
{
    Task<IEnumerable<WishListDetail>>GetAllWishListAsync();
}