using System.ComponentModel.DataAnnotations;

namespace HomeLibrary.Models.Reader;

public class ReaderRegister
{
    //including model validation via Attributes because we are retrieving information from the user
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string UserName { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;
}