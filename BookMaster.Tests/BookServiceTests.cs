using Application.Dto;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using Xunit;
using Application.Services;
using Application.Interfaces;
using Domain.Entities;
using Persistence.Interfaces;
using Application.DTO;
using System.Linq;

public class BookServiceTests
{
    private readonly Mock<IBookRepository> _mockBookRepository;
    private readonly BookService _bookService;

    public BookServiceTests()
    {
        _mockBookRepository = new Mock<IBookRepository>();
        _bookService = new BookService(_mockBookRepository.Object);
    }

     [Fact]
    public void FindBooks_ReturnsFilteredBooks_ByTitle()
    {
        // Arrange
        var books = new List<Book>
        {
            new Book { Title = "Matching Title" },
            new Book { Title = "Other Title" }
        };
        _mockBookRepository.Setup(repo => repo.GetBooks()).Returns(books);

        // Act
        var result = _bookService.FindBooks("Matching", null, null, 0);

        // Assert
        Assert.Single(result.Books);
        Assert.Equal("Matching Title", result.Books[0].Title);
    }

    [Fact]
    public void FindBooks_ReturnsFilteredBooks_ByAuthor()
    {
        // Arrange
        var books = new List<Book>
        {
            new Book { Title = "Book1", BookAuthors = new List<BookAuthor> { new BookAuthor { Author = new Author { Name = "Matching Author" } } } },
            new Book { Title = "Book2", BookAuthors = new List<BookAuthor> { new BookAuthor { Author = new Author { Name = "Other Author" } } } }
        };
        _mockBookRepository.Setup(repo => repo.GetBooks()).Returns(books);

        // Act
        var result = _bookService.FindBooks(null, "Matching", null, 0);

        // Assert
        Assert.Single(result.Books);
        Assert.Equal("Book1", result.Books[0].Title);
    }

    [Fact]
    public void FindBooks_ReturnsFilteredBooks_BySubject()
    {
        // Arrange
        var books = new List<Book>
        {
            new Book { Title = "Book1", BookSubjects = new List<BookSubject> { new BookSubject { Subject = new Subject { Name = "Matching Subject" } } } },
            new Book { Title = "Book2", BookSubjects = new List<BookSubject> { new BookSubject { Subject = new Subject { Name = "Other Subject" } } } }
        };
        _mockBookRepository.Setup(repo => repo.GetBooks()).Returns(books);

        // Act
        var result = _bookService.FindBooks(null, null, "Matching", 0);

        // Assert
        Assert.Single(result.Books);
        Assert.Equal("Book1", result.Books[0].Title);
    }

    [Fact]
    public void FindBooks_ReturnsEmptyList_WhenNoBooksMatch()
    {
        // Arrange
        var books = new List<Book>
        {
            new Book { Title = "Book1" },
            new Book { Title = "Book2" }
        };
        _mockBookRepository.Setup(repo => repo.GetBooks()).Returns(books);

        // Act
        var result = _bookService.FindBooks("Nonexistent", null, null, 1);

        // Assert
        Assert.Empty(result.Books);
        Assert.Equal(0, result.ItemsCount);
    }
}
