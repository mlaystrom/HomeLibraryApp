using HomeLibrary.Models.Book;
using HomeLibrary.Services.Book;
using HomeLibrary.Services.Genre;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HomeLibrary.WebMvc.Controllers;

public class BookController : Controller
{
    //used to do the indirect Db interaction
    //adding an instance of the BookService class to retrieve data from the Db
    private IBookService _service;
    private IGenreService _genreService;

    // the parameters are dependency injections for the book service and genre service
    public BookController(IBookService service, IGenreService genreService)
    {
        _service = service;
        _genreService = genreService;
    }

    //Controller method that is called when the "index" page is requested by a reader(user)
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        //getting a list of books from the Db
        var book = await _service.GetAllBooksAsync();
        return View(book);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        //getting all genres from the _genreService
        var genres = await _genreService.GetAllGenresAsync();
        //Converting the list of genres from _GenreService into a list of SelectList items, each will be an option in a dropdown list
        var selectList = genres.Select(g => new SelectListItem(g.Genre, g.Id.ToString())).ToList();
        //this is a "Dictionary" that is used to pass data from this controller to the Create View
        ViewData["Genres"] = selectList;
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
            Comment = book.Comment,
            DateFinished = book.DateFinished
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
            return RedirectToAction(nameof(Details), new { id = id });

        ModelState.AddModelError("Save Error", "Could not update the book. Please try again.");
        return View(model);
    }

    // GET Delete

    [HttpGet]

    public async Task<IActionResult> Delete(int id)
    {
        BookDetail? book = await _service.GetBooksByIdAsync(id);
        if (book is null)
            return RedirectToAction(nameof(Index));

        return View(book);
    }

    //POST Delete
    [HttpPost]

    [ActionName(nameof(Delete))] //connecting the method Confirm Delete with the Delete Action
    public async Task<IActionResult> ConfirmDelete(int id)
    {
        await _service.DeleteBooksByIdAsync(id);
        return RedirectToAction(nameof(Index));

    }
}