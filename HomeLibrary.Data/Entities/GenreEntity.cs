using System.ComponentModel.DataAnnotations;

namespace HomeLibrary.Data.Entities;

public class GenreEntity
{
    [Key]
    public int Id { get; set; }

    public string Genre { get; set; } = string.Empty;

    public int ReaderId { get; set; } //This property represents the Reader that owns the genre
}