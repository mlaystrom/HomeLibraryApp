using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeLibrary.Data.Entities;

public class BooksEntity
{
    [Key]
    public int Id {get; set; }

    [Required]
    [ForeignKey(nameof(Reader))]
    public int ReaderId { get; set; } 
    public virtual ReaderEntity? Reader {get; set; }

    [Required]
    [MinLength(2), MaxLength(100)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(100)]
    public string Author { get; set; } = string.Empty; 

    [Required]
    [ForeignKey(nameof(Genre))]
    public int GenreId {get; set; }
    public virtual GenreEntity? Genre { get; set; }

    public int SeriesNumber { get; set; }

    public DateTime DateFinished { get; set; }

    [MaxLength(250)]
    public string Comment { get; set; } = string.Empty;
}