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

            // Подгружаем все книги вместе с авторами и темами
            var books = _bookRepository.GetBooks().ToList();

            // Применяем логику фильтрации с помощью CheckMatch
            var filteredBooks = books
                .Where(book => CheckMatch(title, author, subject, book))
                .ToList();

            // Преобразуем книги в DTO
            return filteredBooks.Select(book => new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Subtitle = book.Subtitle,
                BookAuthors = book.BookAuthors.Select(ba => ba.Author.Name).ToList(),
                BookSubjects = book.BookSubjects.Select(bs => bs.Subject.Name).ToList(),
                PublicationDate = book.PublicationDate,
                Description = book.Description
            }).ToList();

        }

        private bool CheckMatch(string title, string author, string subject, Book book)
        {

            bool titleMatch = string.IsNullOrEmpty(title) || 
                      (book.Title != null && book.Title.Contains(title, StringComparison.OrdinalIgnoreCase));

            bool authorMatch = false;
            if(book.BookAuthors.Any()) {
            authorMatch = string.IsNullOrEmpty(author) || 
                            (book.BookAuthors != null && 
                            book.BookAuthors.Any(a => a.Author != null && 
                            a.Author.Name != null && 
                            a.Author.Name.Contains(author, StringComparison.OrdinalIgnoreCase)));
            }
            bool subjectMatch = false;
            if(book.BookSubjects.Any()) {
            subjectMatch = string.IsNullOrEmpty(subject) || 
                                (book.BookSubjects != null && 
                                book.BookSubjects.Any(s => s.Subject != null && 
                                s.Subject.Name != null && 
                                s.Subject.Name.Contains(subject, StringComparison.OrdinalIgnoreCase)));
            }
        
            return titleMatch || authorMatch || subjectMatch;

        }
    }
}
