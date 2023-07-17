using HomeLibrary.Models.WishList;
using HomeLibrary.Services.WishList;
using Microsoft.AspNetCore.Mvc;

namespace HomeLibrary.WebMvc.Controllers;

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

    //WishList method for GET Index endpoint
    [HttpGet]
    public async Task<IActionResult>Index()
    {
        List<WishListDetail> wishlist = (List<WishListDetail>)await _service.GetAllWishListAsync(); //don't understand the fix here
        return View(wishlist);
    }

}