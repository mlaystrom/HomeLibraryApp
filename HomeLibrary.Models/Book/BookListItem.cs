namespace HomeLibrary.Models.Book;

public class BookListItem
{
    public int Id { get; set; }
    public int ReaderId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public int GenreId{ get; set; }
    public string Genre { get; set; } = string.Empty;
    public int SeriesNumber { get; set; }
    public DateTime DateFinished { get; set; }
    public string Comment { get; set; } = string.Empty;
   

}