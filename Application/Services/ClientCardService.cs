using Application.Dto;
using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Mapster;
using Persistence.Interfaces;

namespace Application.Services;

public class ClientCardService(
    IClientCardRepository _clientCardRepository,
    IBookService _bookService) : IClientCardService
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

        if(issue != null && (issue.IssueTo - DateTime.Now).Days < 21 && !issue.IsRenewed)
        {

            issue.RenewReturnDateByWeek();

            await _clientCardRepository.EditClientCard(clientCard!);
        }

        return (true, string.Empty, issue.Adapt<CirculationRecord>());
    }

    public async Task<(bool IsSuccess, string Message, CirculationRecord? Record)> ReturnBook(int clientId, int bookId)
    {
        var clientCard = await _clientCardRepository.GetClientCardById(clientId);

        if(clientCard != null && clientCard.Issues.Count < 5)
        {
            var issue = clientCard.Issues.FirstOrDefault(i => i.BookId == bookId);

            if (issue != null)
            {
                var message = string.Empty;

                if((issue.IssueTo - DateTime.Now).Days < 0)
                {
                    message = "Too late return";
                }

                issue.ReturnDate = DateTime.Now;

                clientCard.Issues.Remove(issue);
                clientCard.Returns.Add(issue);

                await _clientCardRepository.EditClientCard(clientCard);

                return (true, message, issue.Adapt<CirculationRecord>());
            }
        }

        return (false, "Client card or issue not found", new CirculationRecord());
    }

    public async Task<(bool IsSuccess, string Message, CirculationRecord? Record)> IssueBook(int clientId, int bookId)
    {
        var clientCard = await _clientCardRepository.GetClientCardById(clientId);

        if (clientCard != null && clientCard.Issues.Count < 5)
        {
            var book = await _bookService.GetBook(bookId);

            if(book != null)
            {
                var issue = new Issue
                {
                    BookId = bookId,
                    ClientId = clientId,
                    IssueFrom = DateTime.Now,
                    IssueTo = DateTime.Now.AddDays(21),
                    ReturnDate = null,
                    Book = book.Adapt<Book>(),
                    Client = clientCard,
                };

                clientCard.Issues.Add(issue);
                await _clientCardRepository.EditClientCard(clientCard);

                return (true, string.Empty, issue.Adapt<CirculationRecord>());
            }
        }

        return (false, "Client card or book not found, or issues are filled", new CirculationRecord());
    }
}