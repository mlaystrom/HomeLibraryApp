using System.ComponentModel.DataAnnotations;

namespace HomeLibrary.Models.WishList;

public class WishListUpdate
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MinLength(2, ErrorMessage = "{0} must be at least {1} characters long.")]
    [MaxLength(100, ErrorMessage = "{0} must be no more than {1} characters.")]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Author { get; set; } = string.Empty;

    public int SeriesNumber { get; set; }

    public string Genre { get; set; } = string.Empty;
}