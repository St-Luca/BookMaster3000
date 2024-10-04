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

    [HttpGet]
    public async Task<IActionResult> GetClients()
    {
        var clients = await _clientService.GetAllClientsAsync();
        return Ok(clients);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetClient(int id)
    {
        var client = await _clientService.GetClientByIdAsync(id);
        if (client == null)
            return NotFound();

        return Ok(client);
    }

    [HttpPost]
    public async Task<IActionResult> CreateClient(ClientDto clientDto)
    {
        var createdClient = await _clientService.CreateClientAsync(clientDto);
        return CreatedAtAction(nameof(GetClient), new { id = createdClient.Id }, createdClient);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateClient(int id, ClientDto clientDto)
    {
        var updatedClient = await _clientService.UpdateClientAsync(id, clientDto);
        if (updatedClient == null)
            return NotFound();

        return Ok(updatedClient);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClient(int id)
    {
        var success = await _clientService.DeleteClientAsync(id);
        if (!success)
            return NotFound();

        return NoContent();
    }
}

