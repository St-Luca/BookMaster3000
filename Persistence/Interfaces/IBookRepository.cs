using Domain.Entities;

namespace Persistence.interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book> GetBookByIdAsync(int id);
        Task<Book> CreateBookAsync(Book bookDto);
        Task<Book> UpdateBookAsync(int id, Book bookDto);
        Task<bool> DeleteBookAsync(int id);
    }
}