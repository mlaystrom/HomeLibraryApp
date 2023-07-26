using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HomeLibrary.Models.Reader;

namespace HomeLibrary.Models.WishList;

public class WishListCreate
{
   
   
    [Required]
    [MinLength(2, ErrorMessage = "{0} must be at least {1} characters long.")]
    [MaxLength(100, ErrorMessage = "{0} must be no more than {1} characters.")]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Author { get; set; } = string.Empty;

    [Display(Name = "Series Number")]
    public int SeriesNumber { get; set; }

    public string Genre { get; set; } = string.Empty;
}

