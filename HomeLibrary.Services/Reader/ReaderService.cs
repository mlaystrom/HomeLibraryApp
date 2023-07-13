
using HomeLibrary.Data;
using HomeLibrary.Data.Entities;
using HomeLibrary.Models.Reader;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HomeLibrary.Services.Reader;

public class ReaderService : IReaderService
{
    private readonly HomeLibraryDbContext _context;
    private readonly UserManager<ReaderEntity> _userManager;
    private readonly SignInManager<ReaderEntity> _signInManager;

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
        if (await ReaderExistsAsync(model.Email, model.UserName))
            return false;


        ReaderEntity reader = new()
        {
            UserName = model.UserName,
            Email = model.Email
        };
        var createResult = await _userManager.CreateAsync(reader, model.Password);
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

}