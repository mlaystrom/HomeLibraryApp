using System.ComponentModel.DataAnnotations;

namespace HomeLibrary.Models.Reader;

public class ReaderEdit
{
    public int Id { get; set; }
    //including model validation via Attributes because we are retrieving information from the user
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Username")]
    public string UserName { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    [Required]
    [Display(Name = "First Name")]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Last Name")]
    public string LastName { get; set; } = string.Empty;
}