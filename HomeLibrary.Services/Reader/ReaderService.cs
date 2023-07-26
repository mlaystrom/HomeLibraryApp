
using HomeLibrary.Data;
using HomeLibrary.Data.Entities;
using HomeLibrary.Models.Reader;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HomeLibrary.Services.Reader;

public class ReaderService : IReaderService
{
    //fields being injected through constructor
    private readonly HomeLibraryDbContext _context;
    private readonly UserManager<ReaderEntity> _userManager;
    private readonly SignInManager<ReaderEntity> _signInManager;

    //using this to set up Reader service methods
    //the methods communicate with the Db and return formatted C# objects that the controller will use
    public ReaderService(
        HomeLibraryDbContext context,
        UserManager<ReaderEntity> userManager,
        SignInManager<ReaderEntity> signInManager)
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
    }
    public async Task<bool> RegisterReaderAsync(ReaderRegister model)
    {
        Console.WriteLine(await ReaderExistsAsync(model.Email, model.UserName));
        if (await ReaderExistsAsync(model.Email, model.UserName))
            return false;


        ReaderEntity reader = new()
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            UserName = model.UserName,
            Email = model.Email
        };

        //var passwordHasher = new PasswordHasher<ReaderEntity>();
       // reader.Password = passwordHasher.HashPassword(reader, model.Password);

        var createResult = await _userManager.CreateAsync(reader, model.Password);
        foreach (var e in createResult.Errors)
        Console.WriteLine(e.Description);
        return createResult.Succeeded;

    }
    public async Task<bool> LoginAsync(ReaderLogin model)
    {
        //verifies reader exists by username
        var reader = await _userManager.FindByNameAsync(model.UserName);
        if (reader is null)
            return false;

       
        //verfies correct pw was given
       var isValidPassword = await _userManager.CheckPasswordAsync(reader, model.Password);
       if (isValidPassword == false)
            return false;
        //verifies the reader exists
        await _signInManager.SignInAsync(reader, true);//changed to .SignInAsync from .SignInManager
        return true;
    }

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }

    private async Task<bool> ReaderExistsAsync(string email, string username)
    {
        var normalizedEmail = _userManager.NormalizeEmail(email);
        var normalizedUserName = _userManager.NormalizeName(username);

        
        return await _context.Reader.AnyAsync(u =>
        u.NormalizedEmail == normalizedEmail || u.NormalizedUserName == normalizedUserName
      );
    }

    public async Task<IEnumerable<ReaderListItem>> GetAllReadersAsync()
    {
        List<ReaderListItem> readers = await _context.Reader
        .Select(r => new ReaderListItem()
        {
            Id = r.Id,
            Email = r.Email,
            UserName = r.UserName,
            FirstName = r.FirstName,
            LastName = r.LastName
        })
        .ToListAsync();
        return readers;
    }

    public async Task<ReaderDetail?>GetReaderByIdAsync(int id)//changed from readerId to id
    {
        //retrieve the Reader with the given Id from database
        var entity = await _context.Reader.FindAsync(id);//changed from readerId to id
        if(entity is null)
        return null;

        ReaderDetail model = new()
        {
            Id = entity.Id,
            Email = entity.Email,
            UserName = entity.UserName,
            FirstName= entity.FirstName,
            LastName = entity.LastName
        };
        return model;
    }

    public async Task<bool> UpdateReaderAsync(ReaderEdit model)
    {
        var entity = await _context.Reader.FindAsync(model.Id);
        if (entity is null)
        return false;
        //updating the entity's properties
        entity.Id = model.Id;
        entity.UserName = model.UserName;
        entity.FirstName = model.FirstName;
        entity.LastName = model.LastName;
        entity.Email = model.Email;

        
        var numberOfChanges = await _context.SaveChangesAsync();

        return numberOfChanges == 1;
    }

    public async Task<bool> DeleteReaderByIdAsync(int id)
    {
        var entity = await _context.Reader.FindAsync(id);

        if(entity is null)
        return false;
        //telling Dbset to remove the found entity and the changes are then saved to the database
        //returns a boolean that states 1 change made
        _context.Reader.Remove(entity);
        return await _context.SaveChangesAsync() ==1;

    }

}