using HomeLibrary.Models.WishList;
using HomeLibrary.Services.WishList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeLibrary.WebMvc.Controllers;

[Authorize]
public class WishListController : Controller
{
    //instance of the WishListService class needed to retrieve data from the Db
    //created a constructor for the controller that takes an instance of IWishListServie and saves as a private field
    //Using this to do the indirect Db interaction
    private IWishListService _service;
    public WishListController(IWishListService service)
    {
        _service = service;
    }

    //A GET endpoint to get the Create View
    //CREATE View holds the HTML form that will be used to create 
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(WishListCreate model)
    {
        if(!ModelState.IsValid)
        return View(model);

        await _service.CreateWishListAsync(model);

        return RedirectToAction(nameof(Index));
    }

    //WishList method for GET Index endpoint
    [HttpGet]
    public async Task<IActionResult>Index()
    {
        List<WishListDetail> wishlist = (List<WishListDetail>)await _service.GetAllWishListAsync(); //don't understand the fix here
        return View(wishlist);
    }

    //WishList GET for update method
    [HttpGet]
     public async Task<IActionResult> Update(int id)
      {
         WishListDetail wishlist = await _service.GetWishListByIdAsync(id);

        WishListUpdate model = new()
          {
          Id = wishlist.Id,
          Title = wishlist.Title,
          Author = wishlist.Author,
          SeriesNumber = wishlist.SeriesNumber,
          Genre = wishlist.Genre,
           };

           return View(model);
      }

      [HttpPost]
      public async Task<IActionResult> Update (int id, WishListUpdate model)
      {
          if (!ModelState.IsValid)
          return View(model);

          if(await _service.UpdateWishListAsync(model))
              return RedirectToAction(nameof(Index), new { id = id});

          ModelState.AddModelError("Save Error", "Could not update the book details.  Please try again.");
          return View(model);
      } 

    public async Task<IActionResult> Delete(int id)
    {
        WishListDetail? wishlist = await _service.GetWishListByIdAsync(id);
        if (wishlist is null)
            return RedirectToAction(nameof(Index));

        return View(wishlist);
    }

    [HttpPost]
    [ActionName(nameof(ConfirmDelete))]//ActionName connects the method (ConfirmDelete) to the ConfirmDelete action

    public async Task<IActionResult> ConfirmDelete(int id)
    {
        await _service.DeleteWishListByIdAsync(id);
        return RedirectToAction(nameof(Index));
    }

}