using Application.Dto;
using Domain.Entities;
using Application.Interfaces;
using Persistence.Interfaces;

namespace Application.Services;

public class BookService(IBookRepository _bookRepository) : IBookService
{
    public List<BookDto> FindBooks(string title, string author, string subject)
    {
        var books = _bookRepository.GetBooks().ToList();

        var filteredBooks = books
            .Where(book => CheckMatch(title, author, subject, book))
            .ToList();

        return filteredBooks.Select(book => new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            Subtitle = book.Subtitle,
            BookAuthors = book.BookAuthors.Select(ba => ba.Author.Name).ToList(),
            BookSubjects = book.BookSubjects.Select(bs => bs.Subject.Name).ToList(),
            PublicationDate = book.PublicationDate.ToShortDateString(),
            Description = book.Description
        }).ToList();
    }

    private bool CheckMatch(string title, string author, string subject, Book book)
    {

        bool titleMatch = string.IsNullOrEmpty(title) || 
                  (book.Title != null && book.Title.Contains(title, StringComparison.OrdinalIgnoreCase));

        bool authorMatch = false;

        if(book.BookAuthors.Count != 0) 
        {

            var books = _bookRepository.GetBooks().ToList();

            var filteredBooks = books
                .Where(book => CheckMatch(title, author, subject, book))
                .ToList();

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

            bool authorMatch = true;
            if(book.BookAuthors.Any()) {
            authorMatch = string.IsNullOrEmpty(author) || 
                            (book.BookAuthors != null && 
                            book.BookAuthors.Any(a => a.Author != null && 
                            a.Author.Name != null && 
                            a.Author.Name.Contains(author, StringComparison.OrdinalIgnoreCase)));
            }
            bool subjectMatch = true;
            if(book.BookSubjects.Any()) {
            subjectMatch = string.IsNullOrEmpty(subject) || 
                                (book.BookSubjects != null && 
                                book.BookSubjects.Any(s => s.Subject != null && 
                                s.Subject.Name != null && 
                                s.Subject.Name.Contains(subject, StringComparison.OrdinalIgnoreCase)));
            }
        
            return titleMatch && authorMatch && subjectMatch;

        }

    }
}
