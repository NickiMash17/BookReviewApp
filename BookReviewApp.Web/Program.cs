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

// Configure database connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// For Azure, use SQL Server; for local development, use SQLite
if (string.IsNullOrEmpty(connectionString))
{
    // Check if we're running in Azure
    var isAzure = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("WEBSITE_SITE_NAME"));
    
    if (isAzure)
    {
        // Try to get connection string from Azure App Service configuration
        connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");
        
        if (string.IsNullOrEmpty(connectionString))
        {
            // Fallback to hardcoded Azure SQL Database connection string
            var server = "bookreviewapp-sql-1755367448.database.windows.net";
            var database = "BookReviewAppDB";
            var userId = "bookreviewadmin";
            var password = "BookReviewApp2025!";
            
            connectionString = $"Server={server};Database={database};User ID={userId};Password={password};Encrypt=true;TrustServerCertificate=false;";
            Console.WriteLine("Using hardcoded Azure SQL Database connection");
        }
        else
        {
            Console.WriteLine("Using Azure App Service connection string");
        }
    }
    else
    {
        // Fallback to local development with SQLite
        connectionString = "Data Source=BookReviewApp.db";
        Console.WriteLine("Using local SQLite database for development");
    }
}

// Register the appropriate DbContext based on connection string
if (connectionString.Contains("Server=") || connectionString.Contains("database.windows.net"))
{
    // Azure SQL Database
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString));
    Console.WriteLine("SQL Server DbContext registered");
}
else
{
    // Local SQLite
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlite(connectionString));
    Console.WriteLine("SQLite DbContext registered");
}

// Register repositories - use EF Core by default for development
// MongoDB can be enabled by setting "UseMongoDB": true in appsettings.json
var useMongoDB = builder.Configuration.GetValue<bool>("UseMongoDB", false);

if (useMongoDB)
{
    try
    {
        // Try to register MongoDB repositories
        builder.Services.AddScoped<IRepository<Book>, MongoBookRepository>();
        builder.Services.AddScoped<IRepository<Author>, MongoAuthorRepository>();
        Console.WriteLine("MongoDB repositories registered successfully");
    }
    catch (Exception ex)
    {
        // Fallback to EF Core repositories if MongoDB is not available
        Console.WriteLine($"MongoDB not available, falling back to EF Core: {ex.Message}");
        builder.Services.AddScoped<IRepository<Book>, Repository<Book>>();
        builder.Services.AddScoped<IRepository<Author>, Repository<Author>>();
    }
}
else
{
    // Use EF Core repositories by default
    builder.Services.AddScoped<IRepository<Book>, Repository<Book>>();
    builder.Services.AddScoped<IRepository<Author>, Repository<Author>>();
    Console.WriteLine("EF Core repositories registered");
}

// Register EF Core repositories for other entities
builder.Services.AddScoped<IRepository<Review>, Repository<Review>>();
builder.Services.AddScoped<IRepository<User>, Repository<User>>();
builder.Services.AddScoped<IRepository<Category>, Repository<Category>>();
builder.Services.AddScoped<IRepository<BookCategory>, Repository<BookCategory>>();

// Register services
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
        options.ExpireTimeSpan = TimeSpan.FromDays(30);
        options.SlidingExpiration = true;
    });

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHttpsRedirection();
    app.UseHsts();
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

app.Run();

static async Task InitializeDatabaseAsync(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();

    try
    {
        logger.LogInformation("Starting database initialization...");
        
        // Add timeout to prevent hanging
        using var cts = new CancellationTokenSource(TimeSpan.FromMinutes(2));
        
        var context = services.GetRequiredService<ApplicationDbContext>();
        logger.LogInformation("DbContext retrieved successfully");

        // Test basic connection first
        logger.LogInformation("Testing database connection...");
        try
        {
            var connection = context.Database.GetDbConnection();
            await connection.OpenAsync(cts.Token);
            logger.LogInformation($"Database connection successful - Server: {connection.DataSource}, Database: {connection.Database}");
            await connection.CloseAsync();
        }
        catch (Exception connEx)
        {
            logger.LogError(connEx, "Failed to connect to database");
            throw;
        }

        // Always ensure database exists (this will create it if it doesn't exist)
        logger.LogInformation("Ensuring database exists...");
        
        // In Azure production, always recreate the database since it gets deleted on deployment
        var forceRecreation = app.Configuration.GetValue<bool>("ForceDatabaseRecreation", false);
        if (forceRecreation)
        {
            logger.LogInformation("Force database recreation enabled - dropping and recreating database");
            await context.Database.EnsureDeletedAsync(cts.Token);
        }
        
        await context.Database.EnsureCreatedAsync(cts.Token);
        logger.LogInformation("Database created/verified successfully");
        
        // Check if database has data with timeout
        logger.LogInformation("Checking database content...");
        var hasUsers = await context.Users.AnyAsync(cts.Token);
        var hasBooks = await context.Books.AnyAsync(cts.Token);
        var hasAuthors = await context.Authors.AnyAsync(cts.Token);
        
        logger.LogInformation($"Database status - Users: {hasUsers}, Books: {hasBooks}, Authors: {hasAuthors}");
        
        // Always seed data if database is empty (this ensures it works in Azure)
        bool shouldSeedData = !hasUsers || !hasBooks || !hasAuthors;
        logger.LogInformation($"Should seed data: {shouldSeedData}");
        
        if (shouldSeedData)
        {
            logger.LogInformation("Starting data seeding...");
            try
            {
                await BookReviewApp.Data.Seed.SeedData.Initialize<Program>(services, true);
                logger.LogInformation("Data seeding completed successfully");
            }
            catch (Exception seedEx)
            {
                logger.LogError(seedEx, "Error during data seeding, but continuing...");
            }
        }
        else
        {
            logger.LogInformation("Database already has data, skipping seeding");
        }
        
        // Verify database is working with timeout
        logger.LogInformation("Verifying database functionality...");
        var userCount = await context.Users.CountAsync(cts.Token);
        var bookCount = await context.Books.CountAsync(cts.Token);
        var authorCount = await context.Authors.CountAsync(cts.Token);
        
        logger.LogInformation($"Database verification - Users: {userCount}, Books: {bookCount}, Authors: {authorCount}");
        
        logger.LogInformation("Database initialization completed successfully");
    }
    catch (OperationCanceledException)
    {
        logger.LogError("Database initialization timed out after 2 minutes");
        logger.LogWarning("Application will continue without database initialization - some features may not work");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Critical error during database initialization");
        logger.LogWarning("Application will continue without database initialization - some features may not work");
    }
}