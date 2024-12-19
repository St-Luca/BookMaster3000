using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Interfaces;

namespace Persistence.Repositories;

public class BookRepository(LibraryContext _context) : IBookRepository
{
    public async Task<Book?> GetBook(int bookId)
    {
        return await _context.Books
            .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
            .Include(b => b.BookSubjects)
                .ThenInclude(bs => bs.Subject)
            .Include(b => b.Covers)
             .FirstOrDefaultAsync(b => b.Id == bookId);
    }

    public List<Book> GetBooks()
    {
        return _context.Books
            .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
            .Include(b => b.BookSubjects)
                .ThenInclude(bs => bs.Subject)
            .Include(b => b.Covers)
            .ToList();
    }
}