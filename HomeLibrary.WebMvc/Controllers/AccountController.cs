using HomeLibrary.Models.Reader;
using HomeLibrary.Services.Reader;
using Microsoft.AspNetCore.Mvc;

namespace HomeLibrary.WebMvc.Controllers;

public class AccountController : Controller
{
    private readonly IReaderService _readerService;
    public AccountController (IReaderService readerService)
    {
        _readerService = readerService;
    }

    // GET Action for Register -> returns the view to the reader(user)
    public IActionResult Register()
    {
        return View();
    }

    //POST Action for Register -> when the reader(user) submits their data from the view
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(ReaderRegister model)
    {
        // FIRST validate the request model, reject if invalid
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        //Try to register the user, reject if failed
        var registerResult = await _readerService.RegisterReaderAsync(model);
        if (registerResult == false)
        {
            return View(model);
        }

        //Login the new reader(user), redirect to home after
        ReaderLogin loginModel = new()
        {
            UserName = model.UserName,
            Password = model.Password
        };
        await _readerService.LoginAsync(loginModel);
        return RedirectToAction("Index", "Home");

    }

    // GET login
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(ReaderLogin model)
    {
        var loginResult = await _readerService.LoginAsync(model);
        if (loginResult == false)
        {
            return View(model);
        }
        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Logout()
    {
        await _readerService.LogoutAsync();
        return RedirectToAction("Index", "Home");
    }
    
}