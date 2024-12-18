using Domain.Entities;
using Persistence.Interfaces;

namespace Persistence.Repositories;

public class ExhibitionBookRepository(LibraryContext _context) : IExhibitionBookRepository
{
    public async Task AddExhibitionBook(ExhibitionBook exhibition)
    {
        _context.ExhibitionBooks.Add(exhibition);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveExhibitionBook(ExhibitionBook exhibition)
    {
        _context.ExhibitionBooks.Remove(exhibition);
        await _context.SaveChangesAsync();
    }
}
