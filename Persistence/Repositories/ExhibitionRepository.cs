using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Interfaces;

namespace Persistence.Repositories;

public class ExhibitionRepository(LibraryContext _context) : IExhibitionRepository
{
    public async Task CreateExhibition(Exhibition exhibition)
    {
        _context.Exhibitions.Add(exhibition);
        await _context.SaveChangesAsync();
    }

    public async Task EditExhibition(Exhibition exhibition)
    {
        _context.Exhibitions.Update(exhibition);
        await _context.SaveChangesAsync();
    }

    public async Task<Exhibition?> GetExhibition(int id)
    {
        return await _context.Exhibitions
        .Include(e => e.ExhibitionBooks)
            .ThenInclude(eb => eb.Book) 
                .ThenInclude(b => b.Covers) 
            .ThenInclude(eb => eb.Book)
                .ThenInclude(b => b.BookAuthors)
            .ThenInclude(eb => eb.Book)
                .ThenInclude(b => b.BookSubjects)
        .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<List<Exhibition>> GetExhibitions()
    {
        return await _context.Exhibitions
        .Include(e => e.ExhibitionBooks)
            .ThenInclude(eb => eb.Book)
                .ThenInclude(b => b.Covers)
            .ThenInclude(eb => eb.Book)
                .ThenInclude(b => b.BookAuthors)
            .ThenInclude(eb => eb.Book)
                .ThenInclude(b => b.BookSubjects)
        .ToListAsync();
    }
}
