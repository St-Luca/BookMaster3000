using Application.Dto;

namespace Application.interfaces
{
    public interface IBookService
    {
        List<BookDto> FindBooks(string title, string author, string subject);
    }
}