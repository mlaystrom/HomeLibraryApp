using System.Security.Claims;
using HomeLibrary.Data;
using HomeLibrary.Data.Entities;
using HomeLibrary.Models.WishList;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace HomeLibrary.Services.WishList;

public class WishListService : IWishListService
{
    //field that holds HomeLibraryDbContext and injected through a constructor
    private HomeLibraryDbContext _context;
    private int  _readerId;
    public WishListService(IHttpContextAccessor httpContextAccessor, HomeLibraryDbContext context, IConfiguration config)
    {
        var userClaims = httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
        var identifierClaimType = config["ClaimTypes:Id"] ?? "Id";
        var value = userClaims?.FindFirst(identifierClaimType)?.Value;
        var validId = int.TryParse(value, out _readerId);
        if (!validId)
            throw new Exception("Attempted to add book to wishlist without Reader Id claim.");
        _context = context;
    }

    // Creating a wishlist item for list
    public async Task<bool> CreateWishListAsync(WishListCreate model)
    {   //declaring a new WishListEntity
        var entity = new WishListEntity
        {
            ReaderId = _readerId,
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

    public async Task<WishListDetail>GetWishListByIdAsync(int id)
    {
        var entity = await _context.WishList.FindAsync(id);

        if (entity is null)
            return new WishListDetail();
        WishListDetail model = new()
            {
                Id = entity.Id,
                Title = entity.Title,
                Author = entity.Author,
                SeriesNumber = entity.SeriesNumber,
                Genre = entity.Genre
            };
            return model;
        
        
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

    public async Task<bool> DeleteWishListByIdAsync(int id)
    {
        var entity = await _context.WishList.FindAsync(id);

        if (entity is null)
            return false;
        //telling Dbset to remove the found entity and the changes are then saved to the database
        //returns a boolean that states 1 change made
        _context.WishList.Remove(entity);

        var numberOfChanges = await _context.SaveChangesAsync();

        return numberOfChanges == 1;
    }
    


}