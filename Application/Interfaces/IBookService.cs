using Application.Dto;

namespace Application.Interfaces;

public interface IBookService
{
    List<BookDto> FindBooks(string title, string author, string subject);
}