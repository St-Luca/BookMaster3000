using Domain.Entities;

namespace Persistence.Interfaces;

public interface IIssueRepository
{
    Task Update(Issue newIssue);
}
