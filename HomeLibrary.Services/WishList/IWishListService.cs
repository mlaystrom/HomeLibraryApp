using HomeLibrary.Models.WishList;

namespace HomeLibrary.Services.WishList;

public interface IWishListService
{
    Task<bool>CreateWishListAsync(WishListCreate model);
    Task<IEnumerable<WishListDetail>>GetAllWishListAsync();
    Task<bool>UpdateWishListAsync(WishListUpdate model);

}