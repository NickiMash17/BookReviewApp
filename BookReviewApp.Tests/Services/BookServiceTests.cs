using BookReviewApp.Domain.Interfaces;
using BookReviewApp.Domain.Models;
using BookReviewApp.Services.Implementations;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace BookReviewApp.Tests.Services
{
    public class BookServiceTests
    {
        private readonly Mock<IRepository<Book>> _mockBookRepository;
        private readonly BookService _bookService;

        public BookServiceTests()
        {
            _mockBookRepository = new Mock<IRepository<Book>>();
            _bookService = new BookService(_mockBookRepository.Object);
        }

        [Fact]
        public async Task GetAllBooksWithAuthorsAsync_ShouldReturnBooksWithAuthors()
        {
            // Arrange
            var testData = new List<Book>
            {
                new Book
                {
                    BookId = 1,
                    Title = "Test Book 1",
                    Author = new Author { AuthorId = 1, Name = "Test Author 1" }
                },
                new Book
                {
                    BookId = 2,
                    Title = "Test Book 2",
                    Author = new Author { AuthorId = 2, Name = "Test Author 2" }
                }
            };

            _mockBookRepository
                .Setup(repo => repo.GetAllAsync(
                    It.IsAny<Func<IQueryable<Book>, IQueryable<Book>>>(),
                    It.IsAny<Func<IQueryable<Book>, IQueryable<Book>>>()))
                .ReturnsAsync(testData);

            // Act
            var result = await _bookService.GetAllBooksWithAuthorsAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Should().BeEquivalentTo(testData, options => 
                options.Including(b => b.BookId)
                       .Including(b => b.Title)
                       .Including(b => b.Author.AuthorId)
                       .Including(b => b.Author.Name));

            _mockBookRepository.Verify(
                repo => repo.GetAllAsync(
                    It.IsAny<Func<IQueryable<Book>, IQueryable<Book>>>(),
                    It.IsAny<Func<IQueryable<Book>, IQueryable<Book>>>()), 
                Times.Once);
        }
    }
}