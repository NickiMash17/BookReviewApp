using BookReviewApp.Data.Context;
using BookReviewApp.Domain.Models;
using MongoDB.Driver;

namespace BookReviewApp.Data
{
    public static class MongoSeedData
    {
        public static async Task InitializeAsync(MongoDbContext context)
        {
            // Check if data already exists
            var authorCount = await context.Authors.CountDocumentsAsync(_ => true);
            if (authorCount > 0) return; // Data already seeded

            // Seed Authors
            var authors = new List<Author>
            {
                new Author
                {
                    Name = "J.K. Rowling",
                    Biography = "British author best known for the Harry Potter series",
                    DateOfBirth = new DateTime(1965, 7, 31),
                    PhotoUrl = "/images/authors/jk-rowling.jpg"
                },
                new Author
                {
                    Name = "George R.R. Martin",
                    Biography = "American novelist and short story writer",
                    DateOfBirth = new DateTime(1948, 9, 20),
                    PhotoUrl = "/images/authors/george-martin.jpg"
                },
                new Author
                {
                    Name = "Stephen King",
                    Biography = "American author of horror, supernatural fiction, suspense, and fantasy novels",
                    DateOfBirth = new DateTime(1947, 9, 21),
                    PhotoUrl = "/images/authors/stephen-king.jpg"
                }
            };

            await context.Authors.InsertManyAsync(authors);

            // Get the inserted authors to use their IDs
            var insertedAuthors = await context.Authors.Find(_ => true).ToListAsync();

            // Seed Books
            var books = new List<Book>
            {
                new Book
                {
                    Title = "Harry Potter and the Philosopher's Stone",
                    Description = "The first book in the Harry Potter series",
                    ISBN = "9780747532699",
                    PublishedDate = new DateTime(1997, 6, 26),
                    Price = 29.99m,
                    CoverImageUrl = "/images/books/harry-potter-1.jpg",
                    AuthorId = insertedAuthors[0].Id
                },
                new Book
                {
                    Title = "A Game of Thrones",
                    Description = "The first book in A Song of Ice and Fire series",
                    ISBN = "9780553103540",
                    PublishedDate = new DateTime(1996, 8, 1),
                    Price = 34.99m,
                    CoverImageUrl = "/images/books/game-of-thrones.jpg",
                    AuthorId = insertedAuthors[1].Id
                },
                new Book
                {
                    Title = "The Shining",
                    Description = "A horror novel by Stephen King",
                    ISBN = "9780385121675",
                    PublishedDate = new DateTime(1977, 1, 28),
                    Price = 24.99m,
                    CoverImageUrl = "/images/books/the-shining.jpg",
                    AuthorId = insertedAuthors[2].Id
                }
            };

            await context.Books.InsertManyAsync(books);

            // Seed Categories
            var categories = new List<Category>
            {
                new Category { Name = "Fantasy" },
                new Category { Name = "Horror" },
                new Category { Name = "Young Adult" },
                new Category { Name = "Fiction" }
            };

            await context.Categories.InsertManyAsync(categories);

            // Seed Users
            var users = new List<User>
            {
                new User
                {
                    Username = "admin",
                    Email = "admin@bookreview.com",
                    PasswordHash = "hashed_password_here", // In real app, use proper hashing
                    FirstName = "Admin",
                    LastName = "User",
                    IsAdmin = true
                },
                new User
                {
                    Username = "reviewer1",
                    Email = "reviewer1@bookreview.com",
                    PasswordHash = "hashed_password_here",
                    FirstName = "John",
                    LastName = "Doe",
                    IsAdmin = false
                }
            };

            await context.Users.InsertManyAsync(users);
        }
    }
} 