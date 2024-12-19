using Domain.Entities;

namespace Persistence.Interfaces;

public interface IExhibitionBookRepository
{
    Task AddExhibitionBook(ExhibitionBook exhibition);
    Task RemoveExhibitionBook(ExhibitionBook exhibition);
}
