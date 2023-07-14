using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeLibrary.Data.Entities;

public class WishListEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [ForeignKey(nameof(Reader))]
    public int ReaderId { get; set; }
    public ReaderEntity Reader { get; set; }

    [Required]
    [MinLength(2), MaxLength(100)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(100)]
    public string Author { get; set; } = string.Empty;

    public int SeriesNumber { get; set; }

    public string Genre { get; set; } = string.Empty;
}