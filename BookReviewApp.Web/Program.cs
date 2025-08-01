using BookReviewApp.Data;
using BookReviewApp.Data.Context;
using BookReviewApp.Data.Repositories;
using BookReviewApp.Data.Seed;
using BookReviewApp.Domain.Interfaces;
using BookReviewApp.Domain.Models;
using BookReviewApp.Services.Implementations;
using BookReviewApp.Services.Interfaces;
using BookReviewApp.Web.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Microsoft.Extensions.Options;

System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

var builder = WebApplication.CreateBuilder(args);

// Configure MongoDB
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

// Add MongoDB Context
builder.Services.AddSingleton<MongoDbContext>();

// Add services to the container
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Register MongoDB Repositories
// MongoDB repositories (commented out - using EF Core only for now)
// builder.Services.AddScoped<IRepository<Book>, MongoBookRepository>();
// builder.Services.AddScoped<IRepository<Author>, MongoAuthorRepository>();

// Use EF Core repositories for all entities
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddControllersWithViews();

// Add authentication services
builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHttpsRedirection();
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
                b.Id,
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
                    b.Id,
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

    app.MapGet("/debug/mongodb", async (MongoDbContext mongoDb, ILogger<Program> logger) =>
    {
        try
        {
            var books = await mongoDb.Books.Find(_ => true).ToListAsync();
            var authors = await mongoDb.Authors.Find(_ => true).ToListAsync();
            
            return Results.Json(new {
                Status = "MongoDB Connected",
                Database = "BookReviewApp",
                BooksCount = books.Count,
                AuthorsCount = authors.Count,
                Books = books.Select(b => new { b.Id, b.Title, b.AuthorId, b.PublishedDate }),
                Authors = authors.Select(a => new { a.Id, a.Name, a.DateOfBirth })
            });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error checking MongoDB status");
            return Results.Problem("MongoDB check failed. See logs for details.");
        }
    }).WithTags("Debug");

    app.MapGet("/debug/mongodb/test-local", async (ILogger<Program> logger) =>
    {
        try
        {
            var result = await MongoDbTester.TestLocalConnectionAsync();
            return Results.Text(result, "text/plain");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error testing local MongoDB connection");
            return Results.Problem("Local MongoDB test failed. See logs for details.");
        }
    }).WithTags("Debug");

    app.MapGet("/debug/mongodb/test-atlas", async (IConfiguration config, ILogger<Program> logger) =>
    {
        try
        {
            var atlasConnectionString = config.GetValue<string>("AtlasConnectionString");
            if (string.IsNullOrEmpty(atlasConnectionString))
            {
                return Results.Text("❌ Atlas connection string not configured. Please add 'AtlasConnectionString' to your configuration.", "text/plain");
            }
            
            var result = await MongoDbTester.TestAtlasConnectionAsync(atlasConnectionString);
            return Results.Text(result, "text/plain");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error testing Atlas MongoDB connection");
            return Results.Problem("Atlas MongoDB test failed. See logs for details.");
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

    app.MapPost("/debug/mongodb/add-sample-data", async (MongoDbContext mongoDb, ILogger<Program> logger) =>
    {
        try
        {
            // Add sample author (let MongoDB generate the Id)
            var author = new BookReviewApp.Domain.Models.Author
            {
                Name = "J.K. Rowling",
                DateOfBirth = new DateTime(1965, 7, 31),
                Biography = "British author best known for the Harry Potter series"
            };
            await mongoDb.Authors.InsertOneAsync(author);

            // Add sample book (set AuthorId to the inserted author's Id)
            var book = new BookReviewApp.Domain.Models.Book
            {
                Title = "Harry Potter and the Philosopher's Stone",
                AuthorId = author.Id, // author.Id is now the generated ObjectId string
                PublishedDate = new DateTime(1997, 6, 26),
                Description = "The first book in the Harry Potter series",
                ISBN = "978-0747532699",
                Price = 9.99m
            };
            await mongoDb.Books.InsertOneAsync(book);

            return Results.Json(new {
                Message = "Sample data added to MongoDB successfully!",
                Author = new { author.Id, author.Name },
                Book = new { book.Id, book.Title, book.AuthorId }
            });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error adding sample data to MongoDB");
            return Results.Problem("Failed to add sample data. See logs for details.");
        }
    }).WithTags("Debug");

    app.MapPost("/debug/mongodb/migrate-from-sqlite", async (ApplicationDbContext sqliteDb, MongoDbContext mongoDb, ILogger<Program> logger) =>
    {
        int authorsMigrated = 0, booksMigrated = 0, usersMigrated = 0, reviewsMigrated = 0, categoriesMigrated = 0, bookCategoriesMigrated = 0;
        try
        {
            // --- USERS ---
            var sqliteUsers = await sqliteDb.Users.ToListAsync();
            var mongoUsers = await mongoDb.Users.Find(_ => true).ToListAsync();
            var userIdMap = new Dictionary<string, string>(); // SQLite UserId -> Mongo UserId
            foreach (var user in sqliteUsers)
            {
                if (!mongoUsers.Any(u => u.Email == user.Email))
                {
                    var newUser = new BookReviewApp.Domain.Models.User
                    {
                        Email = user.Email,
                        Username = user.Username,
                        PasswordHash = user.PasswordHash,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Role = user.Role,
                        IsActive = user.IsActive,
                        EmailConfirmed = user.EmailConfirmed,
                        LastLoginDate = user.LastLoginDate,
                        ProfilePictureUrl = user.ProfilePictureUrl,
                        IsAdmin = user.IsAdmin
                    };
                    await mongoDb.Users.InsertOneAsync(newUser);
                    usersMigrated++;
                    userIdMap[user.UserId] = newUser.UserId;
                }
                else
                {
                    var existing = mongoUsers.First(u => u.Email == user.Email);
                    userIdMap[user.UserId] = existing.UserId;
                }
            }

            // --- AUTHORS ---
            var sqliteAuthors = await sqliteDb.Authors.ToListAsync();
            var mongoAuthors = await mongoDb.Authors.Find(_ => true).ToListAsync();
            var authorIdMap = new Dictionary<string, string>(); // SQLite Id -> Mongo Id
            foreach (var author in sqliteAuthors)
            {
                if (!mongoAuthors.Any(a => a.Name == author.Name))
                {
                    var newAuthor = new BookReviewApp.Domain.Models.Author
                    {
                        Name = author.Name,
                        DateOfBirth = author.DateOfBirth,
                        Biography = author.Biography,
                        PhotoUrl = author.PhotoUrl
                    };
                    await mongoDb.Authors.InsertOneAsync(newAuthor);
                    authorsMigrated++;
                    authorIdMap[author.Id] = newAuthor.Id;
                }
                else
                {
                    var existing = mongoAuthors.First(a => a.Name == author.Name);
                    authorIdMap[author.Id] = existing.Id;
                }
            }

            // --- CATEGORIES ---
            var sqliteCategories = await sqliteDb.Categories.ToListAsync();
            var mongoCategories = await mongoDb.Categories.Find(_ => true).ToListAsync();
            var categoryIdMap = new Dictionary<string, string>(); // SQLite CategoryId -> Mongo CategoryId
            foreach (var category in sqliteCategories)
            {
                if (!mongoCategories.Any(c => c.Name == category.Name))
                {
                    var newCategory = new BookReviewApp.Domain.Models.Category
                    {
                        Name = category.Name,
                        Description = category.Description
                    };
                    await mongoDb.Categories.InsertOneAsync(newCategory);
                    categoriesMigrated++;
                    categoryIdMap[category.CategoryId] = newCategory.CategoryId;
                }
                else
                {
                    var existing = mongoCategories.First(c => c.Name == category.Name);
                    categoryIdMap[category.CategoryId] = existing.CategoryId;
                }
            }

            // --- BOOKS ---
            var sqliteBooks = await sqliteDb.Books.ToListAsync();
            var mongoBooks = await mongoDb.Books.Find(_ => true).ToListAsync();
            var bookIdMap = new Dictionary<string, string>(); // SQLite Id -> Mongo Id
            foreach (var book in sqliteBooks)
            {
                if (!mongoBooks.Any(b => b.Title == book.Title))
                {
                    var newBook = new BookReviewApp.Domain.Models.Book
                    {
                        Title = book.Title,
                        Description = book.Description,
                        ISBN = book.ISBN,
                        PublishedDate = book.PublishedDate,
                        Price = book.Price,
                        CoverImageUrl = book.CoverImageUrl,
                        AuthorId = authorIdMap.ContainsKey(book.AuthorId) ? authorIdMap[book.AuthorId] : string.Empty
                    };
                    await mongoDb.Books.InsertOneAsync(newBook);
                    booksMigrated++;
                    bookIdMap[book.Id] = newBook.Id;
                }
                else
                {
                    var existing = mongoBooks.First(b => b.Title == book.Title);
                    bookIdMap[book.Id] = existing.Id;
                }
            }

            // --- REVIEWS ---
            var sqliteReviews = await sqliteDb.Reviews.ToListAsync();
            var mongoReviews = await mongoDb.Reviews.Find(_ => true).ToListAsync();
            foreach (var review in sqliteReviews)
            {
                // Check if review already exists by comment and BookId
                var mappedBookId = bookIdMap.ContainsKey(review.BookId) ? bookIdMap[review.BookId] : review.BookId;
                var mappedUserId = userIdMap.ContainsKey(review.UserId) ? userIdMap[review.UserId] : review.UserId;
                if (!mongoReviews.Any(r => r.Comment == review.Comment && r.BookId == mappedBookId))
                {
                    var newReview = new BookReviewApp.Domain.Models.Review
                    {
                        Rating = review.Rating,
                        Comment = review.Comment,
                        BookId = mappedBookId,
                        UserId = mappedUserId,
                        ReviewDate = review.ReviewDate,
                        IsApproved = review.IsApproved
                    };
                    await mongoDb.Reviews.InsertOneAsync(newReview);
                    reviewsMigrated++;
                }
            }

            // --- BOOKCATEGORIES ---
            var sqliteBookCategories = await sqliteDb.BookCategories.ToListAsync();
            var mongoBookCategories = await mongoDb.BookCategories.Find(_ => true).ToListAsync();
            foreach (var bc in sqliteBookCategories)
            {
                // Check if already exists by BookId and CategoryId
                var mappedBookId = bookIdMap.ContainsKey(bc.BookId) ? bookIdMap[bc.BookId] : bc.BookId;
                var mappedCategoryId = categoryIdMap.ContainsKey(bc.CategoryId) ? categoryIdMap[bc.CategoryId] : bc.CategoryId;
                if (!mongoBookCategories.Any(mbc => mbc.BookId == mappedBookId && mbc.CategoryId == mappedCategoryId))
                {
                    var newBookCategory = new BookReviewApp.Domain.Models.BookCategory
                    {
                        BookId = mappedBookId,
                        CategoryId = mappedCategoryId
                    };
                    await mongoDb.BookCategories.InsertOneAsync(newBookCategory);
                    bookCategoriesMigrated++;
                }
            }

            return Results.Json(new {
                Message = "Migration completed!",
                UsersMigrated = usersMigrated,
                AuthorsMigrated = authorsMigrated,
                CategoriesMigrated = categoriesMigrated,
                BooksMigrated = booksMigrated,
                ReviewsMigrated = reviewsMigrated,
                BookCategoriesMigrated = bookCategoriesMigrated
            });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error migrating data from SQLite to MongoDB");
            return Results.Problem("Migration failed. See logs for details.");
        }
    }).WithTags("Debug");

    app.MapGet("/debug/which-mongo", (IOptions<MongoDbSettings> settings) =>
    {
        var useAtlas = settings.Value.UseAtlas;
        var conn = useAtlas ? settings.Value.ConnectionString : settings.Value.LocalConnectionString;
        return Results.Json(new {
            UseAtlas = useAtlas,
            ConnectionString = conn
        });
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Add authentication middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

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
        await BookReviewApp.Data.Seed.SeedData.Initialize<Program>(services, resetData);
        
        // Initialize MongoDB data (commented out for demo - MongoDB not running locally)
        // var mongoContext = services.GetRequiredService<MongoDbContext>();
        // await MongoSeedData.InitializeAsync(mongoContext);
        
        logger.LogInformation("Database initialization completed successfully");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while initializing the database");
        throw;
    }
}