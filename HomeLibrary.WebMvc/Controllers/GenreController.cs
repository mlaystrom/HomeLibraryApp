using HomeLibrary.Models.Genre;
using HomeLibrary.Services.Genre;
using Microsoft.AspNetCore.Mvc;

namespace HomeLibrary.WebMvc.Controllers;

public class GenreController : Controller
{
    private readonly IGenreService _service;

    public GenreController(IGenreService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        List<GenreListItem> genres = await _service.GetAllGenresAsync();
        return View(genres);
    }

    // Returning the Create View
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // POST Create
    // taking in the form data from the Create View (Genre)
    [HttpPost]
    public async Task<IActionResult> Create(GenreCreate model)
    {
        if (!ModelState.IsValid)
            return View(model);

        //calling service method to create a glorious new Genre
        await _service.CreateGenreAsync(model);

        return RedirectToAction(nameof(Index));
    }

    // GET Update method
    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        GenreDetail genre = await _service.GetGenreAsync(id);

        GenreUpdate model = new()
        {
            Id = genre.Id,
            Genre = genre.Genre
        };

        return View(model);
    }


    [HttpPost]
    public async Task<IActionResult> Update(int id, GenreUpdate model)
    {
        if (!ModelState.IsValid)
            return View(model);

        if (await _service.UpdateGenreAsync(model))
            return RedirectToAction(nameof(Index), new { id = id });

        ModelState.AddModelError("Save Error", "Could not update the Genre details.  Please try again.");
        return View(model);
    }

    // GET Delete method
    [HttpGet]

    public async Task<IActionResult> Delete(int id)
    {
        GenreDetail genre = await _service.GetGenreAsync(id);
        if (genre is null)
            return RedirectToAction(nameof(Index));

        return View(genre);
    }

    //POST Delete
    [HttpPost]

    [ActionName(nameof(Delete))] //connecting the method Confirm Delete with the Delete Action
    public async Task<IActionResult> ConfirmDelete(int id)
    {
        await _service.DeleteGenreAsync(id);
        return RedirectToAction(nameof(Index));

    }
}