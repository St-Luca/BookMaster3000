using Application.Dto;
namespace Application.interfaces
{
    public interface IClientService
    {
        Task<IEnumerable<ClientDto>> GetAllClientsAsync();
        Task<ClientDto> GetClientByIdAsync(int id);
        Task<ClientDto> CreateClientAsync(ClientDto clientDto);
        Task<ClientDto> UpdateClientAsync(int id, ClientDto clientDto);
        Task<bool> DeleteClientAsync(int id);
    }
}
