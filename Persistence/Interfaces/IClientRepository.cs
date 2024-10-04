using Domain.Entities;

namespace Persistence.interfaces
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAllClientsAsync();
        Task<Client> GetClientByIdAsync(int id);
        Task<Client> CreateClientAsync(Client client);
        Task<Client> UpdateClientAsync(int id, Client client);
        Task<bool> DeleteClientAsync(int id);
    }

}