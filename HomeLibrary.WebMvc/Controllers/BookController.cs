using HomeLibrary.Models.Book;
using HomeLibrary.Services.Book;
using Microsoft.AspNetCore.Mvc;

namespace HomeLibrary.WebMvc.Controllers;

public class BookController : Controller
{
    //used to do the indirect Db interaction
    //adding an instance of the BookService class to retrieve data from the Db
    private IBookService _service;
    public BookController (IBookService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var book = await _service.GetAllBooksAsync();
        return View(book);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(BookCreate model)
    {
        if (!ModelState.IsValid)
            return View(model);

        await _service.CreateBookAsync(model);

        return RedirectToAction(nameof(Index));
    }
}