using Microsoft.AspNetCore.Identity;

namespace HomeLibrary.Data.Entities;

public class ReaderEntity : IdentityUser<int>
{
    public string? FirstName { get; set; }
    public string? LastName {get; set; }
    //removed UserName and Password because IdentityUser inherited provides inherited property
   
}