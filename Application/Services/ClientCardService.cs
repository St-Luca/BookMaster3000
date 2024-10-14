using Application.Dto;
using Application.Interfaces;
using Domain.Entities;
using Mapster;
using Persistence.Interfaces;

namespace Application.Services;

public class ClientCardService(
    IClientCardRepository _clientCardRepository,
    IIssueRepository _issueRepository) : IClientCardService
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
        var existingClient = await _clientCardRepository.FindClientByPhoneOrEmail(clientData.Phone, clientData.Email);

        if (existingClient != null)
        {
            return (false, "Client with this phone or email already exists", null);
        }

        var newClient = clientData.Adapt<ClientCard>();

        await _clientCardRepository.AddClientCard(newClient);

        return (true, string.Empty, clientData);
    }

    public async Task<(bool IsSuccess, string Message, ClientCardDto? Client)> EditClient(int id, ClientCardDto clientData)
    {
        var client = await _clientCardRepository.GetClientCardById(id);

        if (client == null)
        {
            return (false, "Client not found", null);
        }

        clientData.Adapt(client);

        await _clientCardRepository.EditClientCard(client);

        return (true, string.Empty, clientData);
    }

    public async Task<(bool IsSuccess, string Message)> RenewBook(int clientId, int bookId)
    {
        var clientCard = await _clientCardRepository.GetClientCardById(clientId);

        if (clientCard == null)
        {
            return (false, "Client not found");
        }

        var issue = clientCard.Issues
                                .Where(i => i.BookId == bookId && i.ClientId == clientId)
                                .OrderByDescending(i => i.IssueFrom)
                                .FirstOrDefault();
        if(issue == null)
        {
            return (false, "Issue not found");
        }

        if(issue.IssueTo.Day >= 21)
        {
            return (false, "Issue's days is more than 21");
        }

        issue.IssueTo.AddDays(7);
        await _issueRepository.Update(issue);

        return (true, string.Empty);
    }
}