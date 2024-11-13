using Application.DTO;
namespace Application.Interfaces;

public interface IClientCardService
{
    Task<ClientCardDto?> FindClientById(int id);
    Task<List<ClientCardDto>> FindClientsByName(string name);
    Task<(bool IsSuccess, string Message, ClientCardDto? Client)> AddClient(ClientCardDto clientData);
    Task<(bool IsSuccess, string Message, ClientCardDto? Client)> EditClient(int id, ClientCardDto clientData);
    Task<(bool IsSuccess, string Message, CirculationRecord? Record)> RenewBook(int clientId, int bookId);
    Task<(bool IsSuccess, string Message, CirculationRecord? Record)> ReturnBook(int clientId, int bookId);
    Task<(bool IsSuccess, string Message, CirculationRecord? Record)> IssueBook(int clientId, int bookId);
}
