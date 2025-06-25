using BookReviewApp.Data.Context;
using BookReviewApp.Data.Repositories;
using BookReviewApp.Data.Seed;
using BookReviewApp.Domain.Interfaces;
using BookReviewApp.Services.Implementations;
using BookReviewApp.Services.Interfaces;
using BookReviewApp.Web.Utilities;  // Add this line
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Repository and Services
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    // Debug endpoints for development only
    app.MapGet("/debug/books", async (ApplicationDbContext context) =>
    {
        var books = await context.Books
            .Include(b => b.Author)
            .Select(b => new {
                b.BookId,
                b.Title,
                Author = b.Author.Name,
                b.PublishedDate
            })
            .ToListAsync();
        return Results.Json(books);
    }).WithTags("Debug");

    app.MapGet("/debug/dbcheck", async (ApplicationDbContext db, ILogger<Program> logger) =>
    {
        try
        {
            var books = await db.Books
                .Include(b => b.Author)
                .Select(b => new {
                    b.BookId,
                    b.Title,
                    Author = b.Author.Name,
                    b.PublishedDate
                })
                .ToListAsync();

            var connection = db.Database.GetDbConnection();
            return Results.Json(new {
                Status = "Connected",
                Database = connection.Database,
                Server = connection.DataSource,
                BooksCount = books.Count,
                Books = books
            });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error checking database status");
            return Results.Problem("Database check failed. See logs for details.");
        }
    }).WithTags("Debug");

    app.MapGet("/debug/connection", (IConfiguration config, ILogger<Program> logger) =>
    {
        var connection = config.GetConnectionString("DefaultConnection");
        logger.LogInformation("Connection string requested");
        return Results.Json(new { 
            Connection = connection,
            Provider = "SQLite"
        });
    }).WithTags("Debug");
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Books}/{action=Index}/{id?}");

// Initialize database before starting the application
await InitializeDatabaseAsync(app);

// Generate placeholder images (Windows only)
if (OperatingSystem.IsWindows())
{
    try
    {
        ImageGenerator.CreatePlaceholderImages(app.Environment.WebRootPath);
    }
    catch (Exception ex)
    {
        var logger = app.Services.GetRequiredService<ILogger<Program>>();
        logger.LogWarning(ex, "Failed to generate placeholder images. This is expected on non-Windows platforms.");
    }
}

app.Run();

static async Task InitializeDatabaseAsync(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();

    try
    {
        logger.LogInformation("Initializing database...");
        var context = services.GetRequiredService<ApplicationDbContext>();

        // Ensure database exists and is up to date
        await context.Database.EnsureCreatedAsync();
        
        // Reset and seed data in development
        bool resetData = app.Environment.IsDevelopment();
        await SeedData.Initialize<Program>(services, resetData);
        
        logger.LogInformation("Database initialization completed successfully");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while initializing the database");
        throw;
    }
}