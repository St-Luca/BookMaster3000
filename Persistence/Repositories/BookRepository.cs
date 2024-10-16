using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Interfaces;

namespace Persistence.Repositories;

public class BookRepository(LibraryContext _context) : IBookRepository
{
    public List<Book> GetBooks()
    {
        return _context.Books.Include(b => b.BookAuthors).ThenInclude(ba => ba.Author).Include(b => b.BookSubjects).ThenInclude(bs => bs.Subject).ToList();
    }
}