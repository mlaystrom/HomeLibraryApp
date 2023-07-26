using System.ComponentModel.DataAnnotations;

namespace HomeLibrary.Models.Reader;

public class ReaderLogin
{
    [Required]
    [Display(Name = "Username")]
    public string UserName { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}