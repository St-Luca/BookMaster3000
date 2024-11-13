using Application.DTO;

namespace Application.Interfaces;

public interface IBookService
{
    public byte PaginationLimit { get; }

    BookSearchResult FindBooks(string title, string author, string subject, int page);
    Task<BookDto?> GetBook(int bookId);
}