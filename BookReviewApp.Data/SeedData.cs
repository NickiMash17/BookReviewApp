using BookReviewApp.Data.Context;
using BookReviewApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BookReviewApp.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<ApplicationDbContext>>();

            try
            {
                // Return if data exists
                if (await context.Books.AnyAsync())
                    return;

                // Add sample authors
                var authors = new[]
                {
                    new Author { Name = "George Orwell" },
                    new Author { Name = "J.R.R. Tolkien" },
                    new Author { Name = "Jane Austen" }
                };

                await context.Authors.AddRangeAsync(authors);

                // Add sample books
                var books = new[]
                {
                    new Book { Title = "1984", Description = "A dystopian novel", Author = authors[0] },
                    new Book { Title = "The Hobbit", Description = "A fantasy novel", Author = authors[1] },
                    new Book { Title = "Pride and Prejudice", Description = "A romantic novel", Author = authors[2] }
                };

                await context.Books.AddRangeAsync(books);
                await context.SaveChangesAsync();

                logger.LogInformation("Seed data initialized successfully");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding the database");
                throw;
            }
        }
    }
}