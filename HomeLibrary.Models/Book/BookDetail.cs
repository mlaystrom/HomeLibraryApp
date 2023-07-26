using System.ComponentModel.DataAnnotations;

namespace HomeLibrary.Models.Book;

public class BookDetail
{
    public int Id { get; set; }
    public int ReaderId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public int GenreId { get; set; }
    
    [Display(Name = "Series Number")]
    public int SeriesNumber { get; set; }
    public string Comment {get; set; } = string.Empty;
    [Display(Name = "Date Finished")]
    public DateTime DateFinished { get; set; }
}