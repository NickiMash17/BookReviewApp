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
            // 1. Seed Users (idempotent)
            var reviewerNames = new[]
            {
                "BookLover42", "HorrorFan", "MysteryReader", "FantasyFan83", "LiteratureLover", "JapaneseLitFan",
                "DystopiaReader", "MythologyBuff", "PotterHead", "LiteraryExplorer", "BookwormElite", "ClassicsFan",
                "ModernistReader", "WizardingFan", "HorrorObsessed", "MysteryAddict", "RetroReader", "CasualReader",
                "GoldenAgeFan", "WesterosLover", "RomanceReader"
            };
            var users = new List<User>();
            foreach (var name in reviewerNames)
            {
                if (!context.Users.Any(u => u.Username == name))
                {
                    users.Add(new User
                    {
                        Username = name,
                        Email = $"{name.ToLower()}@example.com",
                        PasswordHash = "hashedpassword",
                        FirstName = name,
                        LastName = "Reviewer",
                        Role = "User",
                        IsActive = true,
                    CreatedAt = DateTime.UtcNow
                    });
                }
            }
            if (users.Count > 0)
            {
                await context.Users.AddRangeAsync(users);
                await context.SaveChangesAsync();
            }
            var userDict = context.Users.ToDictionary(u => u.Username, u => u.UserId);

            // 2. Seed Authors (idempotent)
            var authorData = new[]
            {
                new { Name = "J.K. Rowling", Biography = "British author best known for the Harry Potter series", DateOfBirth = new DateTime(1965, 7, 31), PhotoUrl = "https://upload.wikimedia.org/wikipedia/commons/5/5d/J._K._Rowling_2010.jpg" },
                new { Name = "Stephen King", Biography = "American author of horror, supernatural fiction, suspense", DateOfBirth = new DateTime(1947, 9, 21), PhotoUrl = "https://upload.wikimedia.org/wikipedia/commons/e/e3/Stephen_King%2C_Comicon.jpg" },
                new { Name = "Agatha Christie", Biography = "English writer known for her detective novels", DateOfBirth = new DateTime(1890, 9, 15), PhotoUrl = "https://upload.wikimedia.org/wikipedia/commons/c/cf/Agatha_Christie.png" },
                new { Name = "George R.R. Martin", Biography = "American novelist best known for A Song of Ice and Fire series", DateOfBirth = new DateTime(1948, 9, 20), PhotoUrl = "https://upload.wikimedia.org/wikipedia/commons/e/ed/George_R.R._Martin_by_Gage_Skidmore_2.jpg" },
                new { Name = "Jane Austen", Biography = "English novelist known for her romantic fiction", DateOfBirth = new DateTime(1775, 12, 16), PhotoUrl = "https://upload.wikimedia.org/wikipedia/commons/c/cc/CassandraAusten-JaneAusten%28c.1810%29_hires.jpg" },
                new { Name = "Haruki Murakami", Biography = "Japanese writer known for his surrealist novels", DateOfBirth = new DateTime(1949, 1, 12), PhotoUrl = "https://upload.wikimedia.org/wikipedia/commons/e/eb/Murakami_Haruki_%282009%29.jpg" },
                new { Name = "Margaret Atwood", Biography = "Canadian poet and novelist", DateOfBirth = new DateTime(1939, 11, 18), PhotoUrl = "https://upload.wikimedia.org/wikipedia/commons/7/75/Margaret_Atwood_2015.jpg" },
                new { Name = "Neil Gaiman", Biography = "English author of short fiction, novels, comic books, and films", DateOfBirth = new DateTime(1960, 11, 10), PhotoUrl = "https://upload.wikimedia.org/wikipedia/commons/b/bc/Kyle_Cassidy_-_Neil_Gaiman_2013-2.jpg" },
                new { Name = "Gabriel García Márquez", Biography = "Colombian novelist known for magical realism", DateOfBirth = new DateTime(1927, 3, 6), PhotoUrl = "https://upload.wikimedia.org/wikipedia/commons/0/0f/Gabriel_Garcia_Marquez.jpg" },
                new { Name = "Toni Morrison", Biography = "American novelist and Nobel Prize winner", DateOfBirth = new DateTime(1931, 2, 18), PhotoUrl = "https://upload.wikimedia.org/wikipedia/commons/8/8c/Toni_Morrison_2008-2.jpg" },
                new { Name = "Ernest Hemingway", Biography = "American novelist and short story writer", DateOfBirth = new DateTime(1899, 7, 21), PhotoUrl = "https://upload.wikimedia.org/wikipedia/commons/2/28/ErnestHemingway.jpg" },
                new { Name = "Virginia Woolf", Biography = "English writer and modernist", DateOfBirth = new DateTime(1882, 1, 25), PhotoUrl = "https://upload.wikimedia.org/wikipedia/commons/0/0b/George_Charles_Beresford_-_Virginia_Woolf_in_1902_-_Restoration.jpg" }
            };
            var authors = new List<Author>();
            foreach (var a in authorData)
            {
                if (!context.Authors.Any(x => x.Name == a.Name))
                {
                    authors.Add(new Author { Name = a.Name, Biography = a.Biography, DateOfBirth = a.DateOfBirth, PhotoUrl = a.PhotoUrl, CreatedAt = DateTime.UtcNow });
                }
            }
            if (authors.Count > 0)
            {
            await context.Authors.AddRangeAsync(authors);
            await context.SaveChangesAsync();
            }
            var authorDict = context.Authors.ToDictionary(a => a.Name, a => a.AuthorId);

            // 3. Seed Books (idempotent)
            var bookData = new[]
            {
                new { Title = "Harry Potter and the Philosopher's Stone", Author = "J.K. Rowling", Description = "The first book in the Harry Potter series", ISBN = "9780747532743", PublishedDate = new DateTime(1997, 6, 26), Price = 12.99m, CoverImageUrl = "https://m.media-amazon.com/images/I/81m1s4wIPML._AC_UF1000,1000_QL80_.jpg" },
                new { Title = "The Shining", Author = "Stephen King", Description = "A horror novel about a haunted hotel", ISBN = "9780307743657", PublishedDate = new DateTime(1977, 1, 28), Price = 9.99m, CoverImageUrl = "https://m.media-amazon.com/images/I/71BPuv+iRbL._AC_UF1000,1000_QL80_.jpg" },
                new { Title = "Murder on the Orient Express", Author = "Agatha Christie", Description = "A detective novel featuring Hercule Poirot", ISBN = "9780007119318", PublishedDate = new DateTime(1934, 1, 1), Price = 8.50m, CoverImageUrl = "https://m.media-amazon.com/images/I/91oK9tS7EfL._AC_UF1000,1000_QL80_.jpg" },
                new { Title = "A Game of Thrones", Author = "George R.R. Martin", Description = "The first book in A Song of Ice and Fire series", ISBN = "9780553573404", PublishedDate = new DateTime(1996, 8, 6), Price = 14.99m, CoverImageUrl = "https://m.media-amazon.com/images/I/91dSMhdIzTL._AC_UF1000,1000_QL80_.jpg" },
                new { Title = "Pride and Prejudice", Author = "Jane Austen", Description = "A romantic novel of manners", ISBN = "9780141439518", PublishedDate = new DateTime(1813, 1, 28), Price = 7.99m, CoverImageUrl = "https://m.media-amazon.com/images/I/71Q1tPupKjL._AC_UF1000,1000_QL80_.jpg" },
                new { Title = "Norwegian Wood", Author = "Haruki Murakami", Description = "A nostalgic story of loss and sexuality", ISBN = "9780375704024", PublishedDate = new DateTime(1987, 9, 4), Price = 10.50m, CoverImageUrl = "https://m.media-amazon.com/images/I/81Fsg8ZcOMC._AC_UF1000,1000_QL80_.jpg" },
                new { Title = "The Handmaid's Tale", Author = "Margaret Atwood", Description = "A dystopian novel set in a totalitarian society", ISBN = "9780385490818", PublishedDate = new DateTime(1985, 6, 1), Price = 11.99m, CoverImageUrl = "https://m.media-amazon.com/images/I/91vUn7t3zGL._AC_UF1000,1000_QL80_.jpg" },
                new { Title = "American Gods", Author = "Neil Gaiman", Description = "A novel about a war between old and new gods", ISBN = "9780062572110", PublishedDate = new DateTime(2001, 6, 19), Price = 12.50m, CoverImageUrl = "https://m.media-amazon.com/images/I/81vOOCrKCLL._AC_UF1000,1000_QL80_.jpg" },
                new { Title = "Harry Potter and the Chamber of Secrets", Author = "J.K. Rowling", Description = "The second book in the Harry Potter series", ISBN = "9780747538486", PublishedDate = new DateTime(1998, 7, 2), Price = 12.99m, CoverImageUrl = "https://m.media-amazon.com/images/I/91OINeHnJGL._AC_UF1000,1000_QL80_.jpg" },
                new { Title = "One Hundred Years of Solitude", Author = "Gabriel García Márquez", Description = "A landmark novel of magical realism", ISBN = "9780060883287", PublishedDate = new DateTime(1967, 5, 30), Price = 11.99m, CoverImageUrl = "https://m.media-amazon.com/images/I/91hJ+hgZm4L._AC_UF1000,1000_QL80_.jpg" },
                new { Title = "Beloved", Author = "Toni Morrison", Description = "A powerful examination of slavery, family, and memory", ISBN = "9781400033416", PublishedDate = new DateTime(1987, 9, 2), Price = 10.99m, CoverImageUrl = "https://m.media-amazon.com/images/I/71tZrHXQ+YL._AC_UF1000,1000_QL80_.jpg" },
                new { Title = "The Old Man and the Sea", Author = "Ernest Hemingway", Description = "A short novel about an aging Cuban fisherman", ISBN = "9780684801223", PublishedDate = new DateTime(1952, 9, 1), Price = 8.99m, CoverImageUrl = "https://m.media-amazon.com/images/I/71KloredA3L._AC_UF1000,1000_QL80_.jpg" },
                new { Title = "To the Lighthouse", Author = "Virginia Woolf", Description = "A modernist novel examining family relationships", ISBN = "9780156907392", PublishedDate = new DateTime(1927, 5, 5), Price = 9.50m, CoverImageUrl = "https://m.media-amazon.com/images/I/71YoFhT+9eL._AC_UF1000,1000_QL80_.jpg" },
                new { Title = "Harry Potter and the Prisoner of Azkaban", Author = "J.K. Rowling", Description = "The third book in the Harry Potter series", ISBN = "9780747546290", PublishedDate = new DateTime(1999, 7, 8), Price = 13.99m, CoverImageUrl = "https://m.media-amazon.com/images/I/91Z9xnjKnYL._AC_UF1000,1000_QL80_.jpg" },
                new { Title = "It", Author = "Stephen King", Description = "A horror novel about an ancient, shape-shifting evil", ISBN = "9781501142970", PublishedDate = new DateTime(1986, 9, 15), Price = 12.99m, CoverImageUrl = "https://m.media-amazon.com/images/I/71tFhdcC0XL._AC_UF1000,1000_QL80_.jpg" },
                new { Title = "Death on the Nile", Author = "Agatha Christie", Description = "A mystery novel featuring detective Hercule Poirot", ISBN = "9780062073556", PublishedDate = new DateTime(1937, 11, 1), Price = 8.99m, CoverImageUrl = "https://m.media-amazon.com/images/I/71+ws7RD3mL._AC_UF1000,1000_QL80_.jpg" }
            };
            var books = new List<Book>();
            foreach (var b in bookData)
            {
                if (!context.Books.Any(x => x.Title == b.Title))
                {
                    books.Add(new Book { Title = b.Title, Description = b.Description, ISBN = b.ISBN, PublishedDate = b.PublishedDate, Price = b.Price, CoverImageUrl = b.CoverImageUrl, AuthorId = authorDict[b.Author], CreatedAt = DateTime.UtcNow });
                }
            }
            if (books.Count > 0)
            {
                await context.Books.AddRangeAsync(books);
                await context.SaveChangesAsync();
            }
            var bookDict = context.Books.ToDictionary(b => b.Title, b => b.BookId);

            // 4. Seed Reviews (idempotent)
            var reviewData = new[]
            {
                new { Rating = 5, Comment = "Changed my childhood! Magical and wonderful.", Book = "Harry Potter and the Philosopher's Stone", Reviewer = "BookLover42", ReviewDate = new DateTime(2020, 5, 15) },
                new { Rating = 4, Comment = "Scared me to death but couldn't put it down!", Book = "The Shining", Reviewer = "HorrorFan", ReviewDate = new DateTime(2019, 10, 31) },
                new { Rating = 5, Comment = "Brilliant mystery with an unforgettable ending", Book = "Murder on the Orient Express", Reviewer = "MysteryReader", ReviewDate = new DateTime(2021, 3, 22) },
                new { Rating = 5, Comment = "Epic fantasy at its best. Intricate characters and brutal plot twists.", Book = "A Game of Thrones", Reviewer = "FantasyFan83", ReviewDate = new DateTime(2022, 1, 10) },
                new { Rating = 4, Comment = "A timeless classic with witty dialogue and memorable characters.", Book = "Pride and Prejudice", Reviewer = "LiteratureLover", ReviewDate = new DateTime(2021, 7, 22) },
                new { Rating = 5, Comment = "Beautifully written and melancholic. Murakami at his finest.", Book = "Norwegian Wood", Reviewer = "JapaneseLitFan", ReviewDate = new DateTime(2022, 4, 30) },
                new { Rating = 4, Comment = "Chilling and thought-provoking. Even more relevant today.", Book = "The Handmaid's Tale", Reviewer = "DystopiaReader", ReviewDate = new DateTime(2021, 11, 15) },
                new { Rating = 5, Comment = "A masterpiece of modern mythology. Gaiman blends fantasy and reality perfectly.", Book = "American Gods", Reviewer = "MythologyBuff", ReviewDate = new DateTime(2022, 2, 28) },
                new { Rating = 4, Comment = "The series gets even better! Loved the mystery aspect of this one.", Book = "Harry Potter and the Chamber of Secrets", Reviewer = "PotterHead", ReviewDate = new DateTime(2020, 6, 12) },
                new { Rating = 5, Comment = "A masterpiece of magical realism. Rich and captivating storytelling.", Book = "One Hundred Years of Solitude", Reviewer = "LiteraryExplorer", ReviewDate = new DateTime(2022, 3, 18) },
                new { Rating = 5, Comment = "Haunting and powerful. Morrison's prose is unmatched.", Book = "Beloved", Reviewer = "BookwormElite", ReviewDate = new DateTime(2021, 9, 5) },
                new { Rating = 4, Comment = "Simple yet profound. Hemingway's sparse style at its best.", Book = "The Old Man and the Sea", Reviewer = "ClassicsFan", ReviewDate = new DateTime(2022, 1, 15) },
                new { Rating = 4, Comment = "Woolf's stream of consciousness writing is hypnotic and immersive.", Book = "To the Lighthouse", Reviewer = "ModernistReader", ReviewDate = new DateTime(2021, 11, 30) },
                new { Rating = 5, Comment = "The best in the series! Sirius Black is my favorite character.", Book = "Harry Potter and the Prisoner of Azkaban", Reviewer = "WizardingFan", ReviewDate = new DateTime(2020, 8, 10) },
                new { Rating = 5, Comment = "King's masterpiece. Pennywise will haunt your dreams forever.", Book = "It", Reviewer = "HorrorObsessed", ReviewDate = new DateTime(2021, 10, 25) },
                new { Rating = 4, Comment = "Christie at her best! The twist ending is exceptional.", Book = "Death on the Nile", Reviewer = "MysteryAddict", ReviewDate = new DateTime(2022, 2, 5) },
                new { Rating = 5, Comment = "Re-read this after 20 years and it's still perfect!", Book = "Harry Potter and the Philosopher's Stone", Reviewer = "RetroReader", ReviewDate = new DateTime(2022, 4, 10) },
                new { Rating = 3, Comment = "A bit slow at times but the ending makes up for it.", Book = "The Shining", Reviewer = "CasualReader", ReviewDate = new DateTime(2021, 12, 5) },
                new { Rating = 5, Comment = "Christie's most iconic work for good reason.", Book = "Murder on the Orient Express", Reviewer = "GoldenAgeFan", ReviewDate = new DateTime(2022, 3, 1) },
                new { Rating = 4, Comment = "Not perfect but launched an amazing series.", Book = "A Game of Thrones", Reviewer = "WesterosLover", ReviewDate = new DateTime(2021, 8, 12) },
                new { Rating = 5, Comment = "A perfect romance novel that stands the test of time.", Book = "Pride and Prejudice", Reviewer = "RomanceReader", ReviewDate = new DateTime(2022, 1, 22) }
            };
            var reviews = new List<Review>();
            foreach (var r in reviewData)
            {
                var bookId = bookDict[r.Book];
                var userId = userDict[r.Reviewer];
                if (!context.Reviews.Any(x => x.BookId == bookId && x.UserId == userId && x.Comment == r.Comment))
                {
                    reviews.Add(new Review
                    {
                        Rating = r.Rating,
                        Comment = r.Comment,
                        BookId = bookId,
                        UserId = userId,
                        ReviewDate = r.ReviewDate,
                    CreatedAt = DateTime.UtcNow
                    });
                }
            }
            if (reviews.Count > 0)
            {
                await context.Reviews.AddRangeAsync(reviews);
                await context.SaveChangesAsync();
            }

            // 5. Seed Categories (idempotent)
            var categoryData = new[]
            {
                new { Name = "Fantasy", Description = "Fantasy books" },
                new { Name = "Mystery", Description = "Mystery and detective novels" },
                new { Name = "Classic", Description = "Classic literature" },
                new { Name = "Dystopian", Description = "Dystopian fiction" }
            };
            var categories = new List<Category>();
            foreach (var c in categoryData)
            {
                if (!context.Categories.Any(x => x.Name == c.Name))
                {
                    categories.Add(new Category { Name = c.Name, Description = c.Description, CreatedAt = DateTime.UtcNow });
                }
            }
            if (categories.Count > 0)
            {
                await context.Categories.AddRangeAsync(categories);
                await context.SaveChangesAsync();
            }
            var categoryDict = context.Categories.ToDictionary(c => c.Name, c => c.CategoryId);

            // 6. Assign some books to categories (idempotent)
            var bookCategoryData = new[]
            {
                new { Book = "Harry Potter and the Philosopher's Stone", Category = "Fantasy" },
                new { Book = "A Game of Thrones", Category = "Fantasy" },
                new { Book = "Murder on the Orient Express", Category = "Mystery" },
                new { Book = "Pride and Prejudice", Category = "Classic" },
                new { Book = "Norwegian Wood", Category = "Classic" },
                new { Book = "The Handmaid's Tale", Category = "Dystopian" }
            };
            var bookCategories = new List<BookCategory>();
            foreach (var bc in bookCategoryData)
            {
                var bookId = bookDict[bc.Book];
                var categoryId = categoryDict[bc.Category];
                if (!context.BookCategories.Any(x => x.BookId == bookId && x.CategoryId == categoryId))
                {
                    bookCategories.Add(new BookCategory { BookId = bookId, CategoryId = categoryId, CreatedAt = DateTime.UtcNow });
                }
            }
            if (bookCategories.Count > 0)
            {
                await context.BookCategories.AddRangeAsync(bookCategories);
            await context.SaveChangesAsync();
            }
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