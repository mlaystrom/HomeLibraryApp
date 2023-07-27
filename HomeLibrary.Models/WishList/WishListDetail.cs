using System.ComponentModel.DataAnnotations;

namespace HomeLibrary.Models.WishList;

public class WishListDetail
{
    public int Id { get; set; }
    public int ReaderId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    
    [Display(Name = "Series Number")]
    public int SeriesNumber { get; set; }
    public string Genre { get; set; } = string.Empty;
}
