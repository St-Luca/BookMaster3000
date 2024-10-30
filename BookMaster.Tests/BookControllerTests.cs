using System.Net;
using System.Net.Http.Json;
using Application.Dto;
using Application.DTO;
using Application.Interfaces;
using Persistence.Interfaces;
using Domain.Entities;
using Application.Services;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Xunit;

public class BookServiceIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public BookServiceIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetBook_ReturnsCorrectBook_WhenBookExists()
    {
        // Arrange
        var mockBookRepository = new Mock<IBookRepository>();
        var bookService = new BookService(mockBookRepository.Object);
        
        var book = new Book { Id = 1, Title = "Test Book" };
        mockBookRepository.Setup(repo => repo.GetBook(1)).ReturnsAsync(book);

        // Act
        var result = await bookService.GetBook(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test Book", result?.Title);
    }

    [Fact]
    public async Task GetBook_ReturnsNull_WhenBookDoesNotExist()
    {
        // Arrange
        var mockBookRepository = new Mock<IBookRepository>();
        var bookService = new BookService(mockBookRepository.Object);

        mockBookRepository.Setup(repo => repo.GetBook(It.IsAny<int>())).ReturnsAsync((Book)null);

        // Act
        var result = await bookService.GetBook(9999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void FindBooks_ReturnsPaginatedBooks()
    {
        // Arrange
        var mockBookRepository = new Mock<IBookRepository>();
        var bookService = new BookService(mockBookRepository.Object);

        var books = new List<Book>
        {
            new Book { Id = 1, Title = "Test Book 1" },
            new Book { Id = 2, Title = "Test Book 2" },
            new Book { Id = 3, Title = "Another Book" }
        };
        mockBookRepository.Setup(repo => repo.GetBooks()).Returns(books);

        // Act
        var result = bookService.FindBooks("Book", null, null, page: 0);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.Books.Count);
        Assert.Equal("Test Book 1", result.Books.First().Title);
    }

    [Fact]
    public void FindBooks_FiltersBooksByAuthorAndTitle()
    {
        // Arrange
        var mockBookRepository = new Mock<IBookRepository>();
        var bookService = new BookService(mockBookRepository.Object);

        var books = new List<Book>
        {
            new Book { Id = 1, Title = "Title 1", BookAuthors = new List<BookAuthor> { new BookAuthor { Author = new Author { Name = "John Doe" } } } },
            new Book { Id = 2, Title = "Title 2", BookAuthors = new List<BookAuthor> { new BookAuthor { Author = new Author { Name = "Jane Smith" } } } },
            new Book { Id = 3, Title = "Title 3", BookAuthors = new List<BookAuthor> { new BookAuthor { Author = new Author { Name = "John Doe" } } } }
        };
        mockBookRepository.Setup(repo => repo.GetBooks()).Returns(books);

        // Act
        var result = bookService.FindBooks("Title", "John Doe", null, page: 0);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Books.Count);
    }
}
