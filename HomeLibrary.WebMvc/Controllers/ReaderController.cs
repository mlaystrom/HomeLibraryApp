using HomeLibrary.Models.Reader;
using HomeLibrary.Services.Reader;
using Microsoft.AspNetCore.Mvc;

namespace HomeLibrary.WebMvc.Controllers;

public class ReaderController : Controller
{

private IReaderService _service;
// constructor that takes an instance of some IReaderService and saves to a private field

public ReaderController(IReaderService service)
{
    _service = service;
}

//attempting an index
//GET Index


//Returning detail view for a Reader
[HttpGet]
public async Task<IActionResult> Index(int id)
{
    ReaderDetail? model = await _service.GetReaderByIdAsync(id);

    if(model is null)
    return NotFound();

    return View(model);
}

// Edit Reader Details
// GET Method

[HttpGet]
public async Task<IActionResult> Edit(int id)
{
    ReaderDetail? reader = await _service.GetReaderByIdAsync(id);
    if (reader is null)
    return NotFound();

    ReaderEdit model = new()
    {
    Id = reader.Id,
    UserName = reader.UserName,
    FirstName = reader.FirstName,
    LastName = reader.LastName,
    Email = reader.Email,
   
    };

    return View(model);
}

[HttpPost]
public async Task<IActionResult> Edit(int id, ReaderEdit model)
{
    if (!ModelState.IsValid)
    return View(model);

    //then pass the model to the new service method, if the service returns true and the update passes, return the reader to the Details VIEW

    if(await _service.UpdateReaderAsync(model))
    return RedirectToAction(nameof(Index), new { id = id});

    ModelState.AddModelError("Save Error", "Could not update your details.  Please try again.");
    return View(model);
}

[HttpGet]//forgot to add this method endpoint for Delete

public async Task<IActionResult>Delete(int id)
{
    ReaderDetail? reader = await _service.GetReaderByIdAsync(id);
    if (reader is null)
        return RedirectToAction(nameof(Index));

        return View(reader);
}

[HttpPost]
[ActionName(nameof(ConfirmDelete))]//ActionName connects the method (ConfirmDelete) to the ConfirmDelete action

public async Task<IActionResult> ConfirmDelete(int id)
{
    await _service.DeleteReaderByIdAsync(id);
    return RedirectToAction(nameof(Index));
}

}