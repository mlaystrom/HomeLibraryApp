using HomeLibrary.Data;
using HomeLibrary.Models.WishList;
using Microsoft.EntityFrameworkCore;

namespace HomeLibrary.Services.WishList;

public class WishListService : IWishListService
{
    //field that holds HomeLibraryDbContext and injected through a constructor
    private HomeLibraryDbContext _context;
    public WishListService(HomeLibraryDbContext context)
    {
        _context = context;
    }

    //Returning the collection of WishList books
    public async Task<IEnumerable<WishListDetail>> GetAllWishListAsync()
    {
        List<WishListDetail> wishlist = await _context.WishList
        .Include(w => w.Reader)
        .Select(w => new WishListDetail()
        {
            ReaderId = w.ReaderId,
            Id = w.Id,
            Title = w.Title,
            Author = w.Author,
            SeriesNumber = w.SeriesNumber,
            Genre = w.Genre
        })
        .ToListAsync();//converting selection into a C# list
        return wishlist;
    }


}