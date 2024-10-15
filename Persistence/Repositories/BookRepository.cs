using Domain.Entities;
using Persistence.interfaces;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryContext _context;

        public BookRepository(LibraryContext context)
        {
            _context = context;
        }

        public List<Book> GetBooks()
        {
            return _context.Books.Include(b => b.BookAuthors).ThenInclude(ba => ba.Author).Include(b => b.BookSubjects).ThenInclude(bs => bs.Subject).ToList();
        }
    }
}


