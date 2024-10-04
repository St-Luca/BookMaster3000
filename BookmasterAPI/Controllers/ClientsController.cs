using Microsoft.AspNetCore.Mvc;
using Application.Dto;
using Application.interfaces;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientsController(IClientService clientService)
    {
        _clientService = clientService;
    }

     [HttpGet("{id}")]
    public async Task<IActionResult> GetClientById(string id)
    {
        var client = await _clientService.FindClientById(id);
        if (client == null)
            return NotFound();
        return Ok(client);
    }

    [HttpGet("search")]
    public async Task<IActionResult> GetClientsByName(string name)
    {
        var clients = await _clientService.FindClientsByName(name);
        if (clients == null || !clients.Any())
            return NotFound();
        return Ok(clients);
    }

    [HttpPost]
    public async Task<IActionResult> CreateClient(ClientDto clientDto)
    {
        var result = await _clientService.AddClient(clientDto);
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        return Ok(result.Client);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> EditClient(string id, ClientDto clientData)
    {
        var result = await _clientService.EditClient(id, clientData);
        if (!result.IsSuccess)
            return BadRequest(result.Message);
        return Ok(result.Client);
    }
}

