using Xunit;
using Moq;
using Application.Services;
using Domain.Entities;
using Persistence.interfaces;
using Application.Dto;
using System.Collections.Generic;
using System.Linq;
namespace BookMaster.Tests
{
    public class BookServiceTests
    {
        private readonly BookService _bookService;
        private readonly Mock<IBookRepository> _mockBookRepository;

        public BookServiceTests()
        {
            _mockBookRepository = new Mock<IBookRepository>();

            _bookService = new BookService(_mockBookRepository.Object);

            var books = new List<Book>
            {
                new Book
                {
                    Title = "Harry Potter",
                    Description = "sda",
                    Subtitle = "asdf",
                    BookAuthors = new List<BookAuthor> { new BookAuthor { Author = new Author { Name = "J.K. Rowling" } } },
                    BookSubjects = new List<BookSubject> { new BookSubject { Subject = new Subject { Name = "Fantasy" } } }
                },
                new Book
                {
                    Title = "The Hobbit",
                    Description = "sda",
                    Subtitle = "asdf",
                    BookAuthors = new List<BookAuthor> { new BookAuthor { Author = new Author { Name = "J.R.R. Tolkien" } } },
                    BookSubjects = new List<BookSubject> { new BookSubject { Subject = new Subject { Name = "Fantasy" } } }
                },
                new Book
                {
                    Title = "1984",
                    Description = "sda",
                    Subtitle = "asdf",
                    BookAuthors = new List<BookAuthor> { new BookAuthor { Author = new Author { Name = "George Orwell" } } },
                    BookSubjects = new List<BookSubject> { new BookSubject { Subject = new Subject { Name = "Dystopia" } } }
                }
            };

            _mockBookRepository.Setup(repo => repo.GetBooks()).Returns(books);
        }

        [Fact]
        public void FindBooks_ByTitle_ReturnsCorrectBooks()
        {
            var result = _bookService.FindBooks("Harry Potter", "фыв", "фыв");

            Assert.Single(result);
            Assert.Equal("Harry Potter", result.First().Title);
        }

        [Fact]
        public void FindBooks_ByAuthor_ReturnsCorrectBooks()
        {
            var result = _bookService.FindBooks("фыв", "J.K. Rowling", "фыв");

            foreach (var book in result)
            {
                Console.WriteLine($"Title: {book.Title}, Authors: {string.Join(", ", book.BookAuthors)}");
            }

            Assert.Single(result);
            Assert.Equal("Harry Potter", result.First().Title);
        }

        [Fact]
        public void FindBooks_BySubject_ReturnsCorrectBooks()
        {
            var result = _bookService.FindBooks("фыв", "фыв", "Fantasy");

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void FindBooks_NoMatch_ReturnsEmpty()
        {
            var result = _bookService.FindBooks("NonExistingTitle", "фыв", "фыв");

            Assert.Empty(result);
        }

        [Fact]
        public void FindBooks_ByMultipleCriteria_ReturnsCorrectBooks()
        {
            var result = _bookService.FindBooks("Harry", "J.K. Rowling", "Fantasy");

            Assert.Equal(2, result.Count());
        }
    }
}
