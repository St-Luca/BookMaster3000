using Application.Dto;
namespace Application.interfaces
{
    public interface IClientService
    {
        Task<ClientDto> FindClientById(string id);
        Task<List<ClientDto>> FindClientsByName(string name);
        Task<(bool IsSuccess, string Message, ClientDto Client)> AddClient(ClientDto clientData);
        Task<(bool IsSuccess, string Message, ClientDto Client)> EditClient(string id, ClientDto clientData);
    }
}
