using System.ComponentModel.DataAnnotations;

namespace HomeLibrary.Data.Entities;

public class GenreEntity
{
    [Key]
    public int Id { get; set; }

    public string Genre { get; set; } = string.Empty;
}