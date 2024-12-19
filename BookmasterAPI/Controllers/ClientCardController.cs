using Application.DTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using CsvHelper;
using System.IO;

[ApiController]
[Route("api/[controller]")]
public class ClientCardController(IClientCardService _clientService) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetClientById(int id)
    {
        var client = await _clientService.FindClientById(id);

        return Ok(client ?? new ClientCardDto());
    }

    [HttpGet("search")]
    public async Task<IActionResult> GetClientsByName(string name)
    {
        var clients = await _clientService.FindClientsByName(name);

        return Ok(clients ?? []);
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

    [HttpGet("reminders")]
    public async Task<IActionResult> GetBookReminders()
    {
        var circulationRecords = await _clientService.GetBookReminders();

        return Ok(circulationRecords ?? []);
    }

    [HttpGet("history/{bookId}")]
    public async Task<IActionResult> GetBookCirculationHistory(int bookId)
    {
        var circulationRecords = await _clientService.GetBookCirculationHistory(bookId);

        return Ok(circulationRecords ?? []);
    }

   [HttpGet("export-csv/{bookId}")]
    public async Task<IActionResult> ExportToCsv(int bookId)
    {
        
            var csvFilePath = await _clientService.ExportBookCirculationHistoryToCsv(bookId);

            var fileBytes = await System.IO.File.ReadAllBytesAsync(csvFilePath);
            var fileName = Path.GetFileName(csvFilePath);

            return File(fileBytes, "text/csv", fileName);
        
    }
}