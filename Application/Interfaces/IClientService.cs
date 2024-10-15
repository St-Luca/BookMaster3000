using Application.Dto;
namespace Application.interfaces
{
    public interface IClientService
    {
        Task<ClientDto> FindClientById(int id);
        Task<List<ClientDto>> FindClientsByName(string name);
        Task<(bool IsSuccess, string Message, ClientDto Client)> AddClient(ClientDto clientData);
        Task<(bool IsSuccess, string Message, ClientDto Client)> EditClient(int id, ClientDto clientData);
    }
}
