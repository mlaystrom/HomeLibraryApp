using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeLibrary.Data.Entities;

public class GenreEntity
{
    [Key]
    public int Id { get; set; }

    public string Genre { get; set; } = string.Empty;

    [Required]
    [ForeignKey(nameof(Reader))]
    public int ReaderId { get; set; } //This property represents the Reader that owns the genre
    public virtual ReaderEntity? Reader { get; set; }
}