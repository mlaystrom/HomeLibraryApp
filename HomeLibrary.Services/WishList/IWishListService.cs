using HomeLibrary.Models.WishList;

namespace HomeLibrary.Services.WishList;

public interface IWishListService
{
    Task<bool>CreateWishListAsync(WishListCreate model); // adding a book to wishlist
    Task<IEnumerable<WishListDetail>>GetAllWishListAsync(); //seeing the wishlist
    Task<WishListDetail> GetWishListByIdAsync(int id); //viewing wishlist by Id
    Task<bool>UpdateWishListAsync(WishListUpdate model); //updating the wishlist
    Task<bool> DeleteWishListByIdAsync(int id); //deleting book by id from wishlist

}