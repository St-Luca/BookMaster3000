using Application.Dto;
using Application.Interfaces;
using Domain.Entities;
using Mapster;
using Persistence.Interfaces;

namespace Application.Services;

public class BookService(IBookRepository _bookRepository) : IBookService
{
    public byte PaginationLimit => 50;

    public BookSearchResult FindBooks(string title, string author, string subject, int page)
    {
        var books = _bookRepository.GetBooks().ToList();

        var filteredBooks = books
            .Where(book => CheckMatch(title, author, subject, book))
            .ToList();

        return Paginate(filteredBooks, page);
    }

    public async Task<BookDto?> GetBook(int bookId)
    {
        return (await _bookRepository.GetBook(bookId)).Adapt<BookDto>();
    }

    private bool CheckMatch(string title, string author, string subject, Book book)
    {

        bool titleMatch = string.IsNullOrEmpty(title) ||
                  (book.Title != null && book.Title.Contains(title, StringComparison.OrdinalIgnoreCase));

        bool authorMatch = true;

        if (book.BookAuthors != null && book.BookAuthors.Count != 0)
        {
            authorMatch = string.IsNullOrEmpty(author) ||
                            (book.BookAuthors != null &&
                            book.BookAuthors.Any(a => a.Author != null &&
                            a.Author.Name != null &&
                            a.Author.Name.Contains(author, StringComparison.OrdinalIgnoreCase)));
        }

        bool subjectMatch = true;

        if (book.BookSubjects != null && book.BookSubjects.Count != 0)
        {
            subjectMatch = string.IsNullOrEmpty(subject) ||
                                (book.BookSubjects != null &&
                                book.BookSubjects.Any(s => s.Subject != null &&
                                s.Subject.Name != null &&
                                s.Subject.Name.Contains(subject, StringComparison.OrdinalIgnoreCase)));
        }

        return titleMatch && authorMatch && subjectMatch;
    }

    private BookSearchResult Paginate(List<Book> books, int page)
    {
        return new BookSearchResult
        {
            Books = books.Skip((page - 1) * PaginationLimit).Take(PaginationLimit).Select(b => b.Adapt<BookDto>()).ToList(),
            ItemsCount = books.Count,
            PageLimit = PaginationLimit,
            Page = page,
            Pages = (books.Count + PaginationLimit - 1) / PaginationLimit,
        };
    }
}
