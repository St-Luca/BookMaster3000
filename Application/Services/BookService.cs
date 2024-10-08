using Application.Dto;
using Domain.Entities;
using Application.interfaces;
using Persistence.interfaces;

namespace Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public List<BookDto> FindBooks(string title, string author, string subject)
        {
            var books = _bookRepository.GetBooks();

            return books
                .Where(book => CheckMatch(title, author, subject, book))
                .Select(book => new BookDto
                {
                    Title = book.Title,
                    BookAuthors = book.BookAuthors.Select(a => a.Author.Name).ToList(),
                    BookSubjects = book.BookSubjects.Select(s => s.Subject.Name).ToList(),
                }).ToList();
        }

        private bool CheckMatch(string title, string author, string subject, Book book)
        {
            bool titleMatch = string.IsNullOrEmpty(title) || book.Title.Contains(title, StringComparison.OrdinalIgnoreCase);
            bool authorMatch = string.IsNullOrEmpty(author) || book.BookAuthors.Any(a => a.Author.Name.Contains(author, StringComparison.OrdinalIgnoreCase));
            bool subjectMatch = string.IsNullOrEmpty(subject) || book.BookSubjects.Any(s => s.Subject.Name.Contains(subject, StringComparison.OrdinalIgnoreCase));

            return titleMatch || authorMatch || subjectMatch;
        }
    }
}
