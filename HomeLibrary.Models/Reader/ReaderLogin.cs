using System.ComponentModel.DataAnnotations;

namespace HomeLibrary.Models.Reader;

public class ReaderLogin
{
    [Required]
    public string UserName { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}