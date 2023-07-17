using HomeLibrary.Data;
using HomeLibrary.Data.Entities;
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

    // Creating a wishlist item for list
    public async Task<bool> CreateWishListAsync(WishListCreate model)
    {   //declaring a new WishListEntity
        var entity = new WishListEntity
        {
           
            Title = model.Title,
            Author = model.Author,
            SeriesNumber = model.SeriesNumber,
            Genre = model.Genre
        };
        //adding the new entity to the WishList table
        _context.WishList.Add(entity);
        var numberOfChanges = await _context.SaveChangesAsync();

        return numberOfChanges == 1;
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

    public async Task<bool> UpdateWishListAsync(WishListUpdate model)
    {
        WishListEntity? entity = await _context.WishList.FindAsync(model.Id);

        if (entity is null)
            return false;
            entity.Id = model.Id;
            entity.Title = model.Title;
            entity.Author = model.Author;
            entity.SeriesNumber = model.SeriesNumber;
            entity.Genre = model.Genre;

            return await _context.SaveChangesAsync() == 1;
    }


}