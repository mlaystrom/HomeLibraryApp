using System.ComponentModel.DataAnnotations;

namespace HomeLibrary.Models.Reader;

public class ReaderDetail
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;

    [Display(Name = "Username")]
    public string UserName { get; set; } = string.Empty;
    // public string Password { get; set; } = string.Empty;  //don't need to show this on detail
    [Display(Name = "First Name")]
    public string FirstName { get; set; } = string.Empty;

    [Display(Name = "Last Name")]
     public string LastName { get; set; } = string.Empty;
}