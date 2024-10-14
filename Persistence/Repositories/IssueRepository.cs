using Domain.Entities;
using Persistence.Interfaces;

namespace Persistence.Repositories;

public class IssueRepository(LibraryContext _context) : IIssueRepository
{
    public async Task Update(Issue newIssue)
    {
        _context.Issues.Update(newIssue);
        await _context.SaveChangesAsync();
    }
}
