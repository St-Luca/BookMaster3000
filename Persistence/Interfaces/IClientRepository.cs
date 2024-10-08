using Domain.Entities;

namespace Persistence.interfaces
{
    public interface IClientRepository
    {
        Task<Client> GetClientById(int id);
        Task<List<Client>> GetAllClients();
        Task AddClient(Client client);
        Task EditClient(Client client);
        Task<Client> FindClientByPhoneOrEmail(string phone, string email);
    }

}