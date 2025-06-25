using BookReviewApp.Data.Context;
using BookReviewApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BookReviewApp.Data.Seed
{
    public static class SeedData
    {
        private const string DefaultPlaceholderImage = "/images/placeholder.jpg";
        private const string CoverImagePath = "/images/books";
        private const string AuthorImagePath = "/images/authors";

        public static async Task Initialize<T>(IServiceProvider serviceProvider, bool resetData = false) where T : class
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var logger = serviceProvider.GetRequiredService<ILogger<T>>();

            try
            {
                if (resetData)
                {
                    logger.LogInformation("Resetting database data...");
                    await ResetDatabaseAsync(context);
                }

                if (!await context.Authors.AnyAsync())
                {
                    logger.LogInformation("Seeding sample data...");
                    await SeedSampleDataAsync(context);
                    logger.LogInformation("Sample data seeded successfully");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding the database");
                throw;
            }
        }

        private static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            // Add authors with more complete data
            var authors = new[]
            {
                new Author
                {
                    Name = "George Orwell",
                    Biography = "English novelist and essayist, born Eric Arthur Blair, known for his dystopian novel '1984' and allegorical novella 'Animal Farm'.",
                    DateOfBirth = new DateTime(1903, 6, 25),
                    PhotoUrl = "https://randomuser.me/api/portraits/men/32.jpg",
                    CreatedAt = DateTime.UtcNow
                },
                new Author
                {
                    Name = "J.R.R. Tolkien",
                    Biography = "English writer, poet, and philologist, best known for his high fantasy works 'The Hobbit' and 'The Lord of the Rings'.",
                    DateOfBirth = new DateTime(1892, 1, 3),
                    PhotoUrl = "https://randomuser.me/api/portraits/men/45.jpg",
                    CreatedAt = DateTime.UtcNow
                }
            };

            await context.Authors.AddRangeAsync(authors);
            await context.SaveChangesAsync();

            // Add more books per author
            var books = new[]
            {
                // Orwell's books
                new Book
                {
                    Title = "1984",
                    Description = "A dystopian novel set in a totalitarian society, following Winston Smith's rebellion against the omnipresent government surveillance.",
                    ISBN = "978-0451524935",
                    PublishedDate = new DateTime(1949, 6, 8),
                    Price = 9.99m,
                    CoverImageUrl = "https://covers.openlibrary.org/b/id/10523338-L.jpg",
                    AuthorId = authors[0].AuthorId,
                    CreatedAt = DateTime.UtcNow
                },
                new Book
                {
                    Title = "Animal Farm",
                    Description = "An allegorical novella reflecting events leading up to the Russian Revolution of 1917 and the Stalinist era of the Soviet Union.",
                    ISBN = "978-0451526342",
                    PublishedDate = new DateTime(1945, 8, 17),
                    Price = 8.99m,
                    CoverImageUrl = "https://covers.openlibrary.org/b/id/11153227-L.jpg",
                    AuthorId = authors[0].AuthorId,
                    CreatedAt = DateTime.UtcNow
                },
                // Tolkien's books
                new Book
                {
                    Title = "The Hobbit",
                    Description = "A fantasy novel about the adventures of hobbit Bilbo Baggins, who embarks on a quest to help a group of dwarves reclaim their mountain home.",
                    ISBN = "978-0547928227",
                    PublishedDate = new DateTime(1937, 9, 21),
                    Price = 12.99m,
                    CoverImageUrl = "https://covers.openlibrary.org/b/id/10958330-L.jpg",
                    AuthorId = authors[1].AuthorId,
                    CreatedAt = DateTime.UtcNow
                },
                new Book
                {
                    Title = "The Fellowship of the Ring",
                    Description = "The first volume of The Lord of the Rings trilogy, following Frodo Baggins as he and a fellowship embark on a quest to destroy the One Ring.",
                    ISBN = "978-0547928210",
                    PublishedDate = new DateTime(1954, 7, 29),
                    Price = 14.99m,
                    CoverImageUrl = "https://covers.openlibrary.org/b/id/8231856-L.jpg",
                    AuthorId = authors[1].AuthorId,
                    CreatedAt = DateTime.UtcNow
                }
            };

            await context.Books.AddRangeAsync(books);
            await context.SaveChangesAsync();
        }

        private static async Task ResetDatabaseAsync(ApplicationDbContext context)
        {
            // Remove existing data in reverse order of dependencies
            context.Books.RemoveRange(await context.Books.ToListAsync());
            context.Authors.RemoveRange(await context.Authors.ToListAsync());
            await context.SaveChangesAsync();
        }
    }
}