using Domain.Entities;

namespace Persistence.Interfaces
{
    public interface IClientCardRepository
    {
        Task<ClientCard?> GetClientCardById(int id);
        Task<List<ClientCard>> GetAllClientCards();
        Task AddClientCard(ClientCard client);
        Task EditClientCard(ClientCard client);
        Task<ClientCard?> FindClientByPhoneOrEmail(string phone, string email);
    }

}