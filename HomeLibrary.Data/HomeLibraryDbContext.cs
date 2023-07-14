using HomeLibrary.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HomeLibrary.Data;

public class HomeLibraryDbContext : IdentityDbContext<ReaderEntity, IdentityRole<int>, int>
{
    public HomeLibraryDbContext(DbContextOptions<HomeLibraryDbContext> options)
        : base(options) { }
    //public DbSet<ReaderEntity> Reader {get; set; }
    public virtual DbSet<BooksEntity> Book { get; set; }

    public virtual DbSet<GenreEntity> Genre { get; set; }

    public virtual DbSet<ReaderEntity> Reader { get; set; }

    public virtual DbSet<WishListEntity> WishList { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ReaderEntity>().ToTable("Reader");
            modelBuilder.Entity<GenreEntity>().ToTable("Genre");
            modelBuilder.Entity<BooksEntity>().ToTable("Book");
            modelBuilder.Entity<WishListEntity>().ToTable("WishList");
        
    }
}