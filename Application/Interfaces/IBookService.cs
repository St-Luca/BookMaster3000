using Application.Dto;

namespace Application.Interfaces;

public interface IBookService
{
    public byte PaginationLimit { get; }

    BookSearchResult FindBooks(string title, string author, string subject, int page);
}