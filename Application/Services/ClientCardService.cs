using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Mapster;
using Persistence.Interfaces;

namespace Application.Services;

public class ClientCardService(
    IClientCardRepository _clientCardRepository,
    IBookRepository _bookRepository,
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
        if (clientData != null && clientData.Name != ""){
            var newClient = clientData.Adapt<ClientCard>();
            await _clientCardRepository.AddClientCard(newClient);

            return (true, string.Empty, clientData);
        }
        return (false, "Invalid client data", clientData);
    }

    public async Task<(bool IsSuccess, string Message, ClientCardDto? Client)> EditClient(int id, ClientCardDto clientData)
    {
        var client = await _clientCardRepository.GetClientCardById(id);

        if (client != null)
        {
            clientData.Adapt(client);

            await _clientCardRepository.EditClientCard(client);

            return (true, string.Empty, clientData);
        }

        return (false, "Client not found", clientData);
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

            return (true, string.Empty, issue.Adapt<CirculationRecord>());
        }

        return (false, "Book has already been renewed", issue.Adapt<CirculationRecord>());
    }

    public async Task<(bool IsSuccess, string Message, CirculationRecord? Record)> ReturnBook(int clientId, int bookId)
    {
        var clientCard = await _clientCardRepository.GetClientCardById(clientId);

        if(clientCard != null  && clientCard.Issues != null && clientCard.Issues.Count < 5)
        {
            var issue = clientCard.Issues.FirstOrDefault(i => i.BookId == bookId);

            if (issue != null)
            {
                var message = string.Empty;

                if((issue.IssueTo - DateTime.Now.ToUniversalTime()).Days < 0)
                {
                    message = "Too late return";
                }

                issue.ReturnDate = DateTime.Now.ToUniversalTime();

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
            var book = await _bookRepository.GetBook(bookId);

            if(book != null)
            {
                var issue = new Issue
                {
                    BookId = bookId,
                    ClientCardId = clientId,
                    IssueFrom = DateTime.Now.ToUniversalTime(),
                    IssueTo = DateTime.Now.AddDays(21).ToUniversalTime(),
                    ReturnDate = null,
                    Book = book,
                    ClientCard = clientCard,
                };

                clientCard.Issues.Add(issue);

                await _clientCardRepository.EditClientCard(clientCard);

                return (true, string.Empty, issue.Adapt<CirculationRecord>());
            }
        }

        return (false, "Client card or book not found, or issues are filled", new CirculationRecord());
    }

    public async Task<List<CirculationRecord>> GetBookReminders()
    {
        var clients = await _clientCardRepository.GetAllClientCards();

        return clients
                .SelectMany(client => client.Issues)
                .Select(issue => issue.Adapt<CirculationRecord>())
                .ToList();
    }

    public async Task<List<CirculationRecord>> GetBookCirculationHistory(int bookId)
    {
        var clients = await _clientCardRepository.GetAllClientCards();

        var circulationRecords = clients
                                    .SelectMany(client => client.Returns.Concat(client.Issues))
                                    .Where(i => i.BookId == bookId)                             
                                    .OrderByDescending(i => i.IssueFrom)                       
                                    .Select(issue => issue.Adapt<CirculationRecord>())         
                                    .ToList();

        return circulationRecords;
    }
}