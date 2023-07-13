using Microsoft.AspNetCore.Identity;

namespace HomeLibrary.Data.Entities;

public class ReaderEntity : IdentityUser<int>
{
    public string? FirstName { get; set; }
    public string? LastName {get; set; }
    //removed UserName because IdentityUser inherited provides inherited property
    public string? Password {get; set; }
}