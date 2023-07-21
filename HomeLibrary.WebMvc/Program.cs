using HomeLibrary.Data;
using HomeLibrary.Data.Entities;
using HomeLibrary.Services.Book;
using HomeLibrary.Services.Genre;
using HomeLibrary.Services.Reader;
using HomeLibrary.Services.WishList;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// DbContext configuration, adds the DbContext for dependency injection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<HomeLibraryDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

// builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IReaderService, ReaderService>();
builder.Services.AddScoped<IWishListService, WishListService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IGenreService, GenreService>();

// Enables using Identity Managers 
builder.Services.AddDefaultIdentity<ReaderEntity>()
    .AddEntityFrameworkStores<HomeLibraryDbContext>();

// Configure what happens when a logged out user tries to access an authorized route
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
});

    
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
