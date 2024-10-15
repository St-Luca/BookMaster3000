using Domain.Entities;

namespace Persistence.interfaces
{
    public interface IBookRepository
    {
        List<Book> GetBooks();
    }
}