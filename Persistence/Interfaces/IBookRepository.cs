using Domain.Entities;

namespace Persistence.Interfaces;

public interface IBookRepository
{
    List<Book> GetBooks();
    Task<Book?> GetBook(int bookId);
}