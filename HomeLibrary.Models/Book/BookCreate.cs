using System.ComponentModel.DataAnnotations;

namespace HomeLibrary.Models.Book;

public class BookCreate
{
    public int Id { get; set; }
    public int ReaderId { get; set; }

    [Required]
    [MinLength(2, ErrorMessage = "{0} must be at least {1} characters long.")]
    [MaxLength(100, ErrorMessage = "{0} must be no more than {1} characters.")]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Author { get; set; } = string.Empty;
    public int GenreId { get; set; }
   
    public int SeriesNumber { get; set; }
    public DateTime DateFinished { get; set; }

    [MaxLength(250)]
    public string Comment { get; set; } = string.Empty;
}