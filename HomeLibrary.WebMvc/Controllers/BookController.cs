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
}