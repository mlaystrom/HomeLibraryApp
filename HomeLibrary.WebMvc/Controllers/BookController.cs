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

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        //calling the service method and storing the detail model
        BookDetail? model = await _service.GetBooksByIdAsync(id);

        //if not found, will be returned to the not found view
        if (model is null)
        return NotFound();

        //if found, returns the model VIEW
        return View(model);
    }

    // GET Update

    [HttpGet]

    public async Task<IActionResult> Update(int id)
    {
        BookDetail? book = await _service.GetBooksByIdAsync(id);
        if (book is null)
        return NotFound();

        //if not null, will now create a BookUpdate model from the Book data object to load into view
        BookUpdate model = new()
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            SeriesNumber = book.SeriesNumber,
            GenreId = book.GenreId,
            Comment = book.Comment
        };
        return View(model);
    }

        // POST Update

        [HttpPost]
        
        // method takes in the Id of the Book AND the BookUpdate model because it contains the new data
        public async Task<IActionResult> Update(int id, BookUpdate model)
        {
            if (!ModelState.IsValid)
            return View(model);

            if (await _service.UpdateBookAsync(model))
            return RedirectToAction(nameof(Details), new {id = id});

            ModelState.AddModelError("Save Error", "Could not update the book. Please try again.");
            return View(model);
        }

    // GET Delete

    [HttpGet]
    
    public async Task<IActionResult> Delete (int id)
    {
        BookDetail? book = await _service.GetBooksByIdAsync(id);
        if (book is null)
            return RedirectToAction(nameof(Index));
        
        return View(book);
    }

    //POST Delete
    
}