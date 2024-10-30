using Application.Dto;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ClientCardController(IClientCardService _clientService) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetClientById(int id)
    {
        var client = await _clientService.FindClientById(id);

        if (client == null)
        {
            return Ok(new ClientCardDto());
        }

        return Ok(client);
    }

    [HttpGet("search")]
    public async Task<IActionResult> GetClientsByName(string name)
    {
        var clients = await _clientService.FindClientsByName(name);

        if (clients == null || !clients.Any())
        {
            return Ok(new List<ClientCardDto>());
        }

        return Ok(clients);
    }

    [HttpPost]
    public async Task<IActionResult> CreateClient(ClientCardDto clientDto)
    {
        var result = await _clientService.AddClient(clientDto);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return Ok(result.Client);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> EditClient(int id, ClientCardDto clientData)
    {
        var result = await _clientService.EditClient(id, clientData);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return Ok(result.Client);
    }

    [HttpPut("{clientId}/{bookId}")]
    public async Task<IActionResult> RenewBook(int clientId, int bookId)
    {
        var result = await _clientService.RenewBook(clientId, bookId);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return Ok(result.Record);
    }

    [HttpPost("{clientId}/{bookId}/issue")]
    public async Task<IActionResult> IssueBook(int clientId, int bookId)
    {
        var result = await _clientService.IssueBook(clientId, bookId);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return Ok(result.Record);
    }

    [HttpPost("{clientId}/{bookId}/return")]
    public async Task<IActionResult> ReturnBook(int clientId, int bookId)
    {
        var result = await _clientService.ReturnBook(clientId, bookId);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return Ok(result.Record);
    }
}