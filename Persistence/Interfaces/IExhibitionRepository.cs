using Domain.Entities;

namespace Persistence.Interfaces;

public interface IExhibitionRepository
{
    Task<Exhibition?> GetExhibition(int id);
    Task<List<Exhibition>> GetExhibitions();
    Task CreateExhibition(Exhibition exhibition);
    Task EditExhibition(Exhibition exhibition);
}
