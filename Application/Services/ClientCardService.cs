using Application.Dto;
using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Mapster;
using Persistence.Interfaces;

namespace Application.Services;

public class ClientCardService(IClientCardRepository _clientCardRepository) : IClientCardService
{
    public async Task<ClientCardDto?> FindClientById(int id)
    {
        var client = await _clientCardRepository.GetClientCardById(id);

        if (client == null) return null;

        return client.Adapt<ClientCardDto>();
    }

    public async Task<List<ClientCardDto>> FindClientsByName(string name)
    {
        var clients = await _clientCardRepository.GetAllClientCards();

        return clients
                .Where(c => c.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                .Adapt<List<ClientCardDto>>();
    }

    public async Task<(bool IsSuccess, string Message, ClientCardDto? Client)> AddClient(ClientCardDto clientData)
    {
        var newClient = clientData.Adapt<ClientCard>();
        await _clientCardRepository.AddClientCard(newClient);

        return (true, string.Empty, clientData);
    }

    public async Task<(bool IsSuccess, string Message, ClientCardDto? Client)> EditClient(int id, ClientCardDto clientData)
    {
        var client = await _clientCardRepository.GetClientCardById(id);

        if (client != null)
        {
            clientData.Adapt(client);

            await _clientCardRepository.EditClientCard(client);
        }

        return (true, string.Empty, clientData);
    }

    public async Task<(bool IsSuccess, string Message, CirculationRecord? Record)> RenewBook(int clientId, int bookId)
    {
        var clientCard = await _clientCardRepository.GetClientCardById(clientId);
        Issue? issue = null;

        if (clientCard != null)
        {
            issue = clientCard.Issues.Where(i => i.BookId == bookId).FirstOrDefault();
        }

        if(issue != null && (issue.IssueTo - DateTime.Now).Days < 21)
        {
            issue.IssueTo.AddDays(7);

            await _clientCardRepository.EditClientCard(clientCard!);
        }

        return (true, string.Empty, issue.Adapt<CirculationRecord>());
    }
}