using System.Net;
using System.Net.Http.Json;
using Application.Dto;
using Application.DTO;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Application.Interfaces;
using Domain.Entities;
using Xunit;

public class ClientCardServiceIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ClientCardServiceIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateClient_ReturnsCreatedClient()
    {
        // Arrange
        var newClient = new ClientCardDto { Name = "Jane Doe", Email = "jane@example.com", Phone = "1234567890" };

        // Act
        var response = await _client.PostAsJsonAsync("http://localhost:8080/api/СlientСard", newClient);

        // Assert
        response.EnsureSuccessStatusCode();
        var createdClient = await response.Content.ReadFromJsonAsync<ClientCardDto>();
        Assert.NotNull(createdClient);
        Assert.Equal(newClient.Name, createdClient.Name);
    }

    [Fact]
    public async Task GetClientById_ReturnsClient_WhenClientExists()
    {
        // Arrange
        var createdClient = await CreateTestClient();

        // Act
        var response = await _client.GetAsync($"http://localhost:8080/api/clientcard/{createdClient.Id}");

        // Assert
        response.EnsureSuccessStatusCode();
        var client = await response.Content.ReadFromJsonAsync<ClientCardDto>();
        Assert.NotNull(client);
        Assert.Equal(createdClient.Name, client.Name);
    }

    [Fact]
    public async Task EditClient_UpdatesClient_WhenClientExists()
    {
        // Arrange
        var client = await CreateTestClient();
        var updatedClientData = new ClientCardDto { Name = "Updated Name", Email = client.Email, Phone = client.Phone };

        // Act
        var response = await _client.PutAsJsonAsync($"http://localhost:8080/api/ClientCard/{client.Id}", updatedClientData);

        // Assert
        response.EnsureSuccessStatusCode();
        var resultClient = await response.Content.ReadFromJsonAsync<ClientCardDto>();
        Assert.Equal("Updated Name", resultClient.Name);
    }

    [Fact]
    public async Task GetClientsByName_ReturnsMatchingClients()
    {
        // Arrange
        await CreateTestClient("John Doe");
        await CreateTestClient("Jane Doe");

        // Act
        var response = await _client.GetAsync("http://localhost:8080/api/ClientCard/search?name=Doe");

        // Assert
        response.EnsureSuccessStatusCode();
        var clients = await response.Content.ReadFromJsonAsync<List<ClientCardDto>>();
        Assert.NotNull(clients);
        Assert.True(clients.Count >= 2);
    }

    [Fact]
    public async Task IssueBook_ReturnsSuccess_WhenBookIsIssued()
    {
        // Arrange
        var client = await CreateTestClient();
        var bookId = 1; // Assuming a book with ID 1 exists or create a test book

        // Act
        var response = await _client.PostAsync($"http://localhost:8080/api/clientcard/{client.Id}/{bookId}/issue", null);

        // Assert
        response.EnsureSuccessStatusCode();
        var record = await response.Content.ReadFromJsonAsync<CirculationRecord>();
        Assert.NotNull(record);
    }

    [Fact]
    public async Task RenewBook_RenewsBookForClient()
    {
        // Arrange
        var client = await CreateTestClient();
        var bookId = 1; // Assuming a book with ID 1 exists

        // Issue the book first
        await _client.PostAsync($"http://localhost:8080/api/clientcard/{client.Id}/{bookId}/issue", null);

        // Act
        var response = await _client.PutAsync($"http://localhost:8080/api/clientcard/{client.Id}/{bookId}", null);

        // Assert
        response.EnsureSuccessStatusCode();
        var record = await response.Content.ReadFromJsonAsync<CirculationRecord>();
        Assert.NotNull(record);
    }

    [Fact]
    public async Task ReturnBook_ReturnsSuccess_WhenBookIsReturned()
    {
        // Arrange
        var client = await CreateTestClient();
        var bookId = 1; // Assuming a book with ID 1 exists

        // Issue the book first
        await _client.PostAsync($"http://localhost:8080/api/ClientCard/{client.Id}/{bookId}/issue", null);

        // Act
        var response = await _client.PostAsync($"http://localhost:8080/api/ClientCard/{client.Id}/{bookId}/return", null);

        // Assert
        response.EnsureSuccessStatusCode();
        var record = await response.Content.ReadFromJsonAsync<CirculationRecord>();
        Assert.NotNull(record);
        Assert.NotNull(record.ReturnDate);
    }

    private async Task<ClientCardDto> CreateTestClient(string name = "Test User")
    {
        var clientDto = new ClientCardDto
        {
            Id = 0,
            Name = name,
            Zip = "12345",
            City = "Test City",
            Phone = "1234567890",
            Email = $"{name.ToLower().Replace(" ", "")}@example.com",
            Address = "123 Test Street"
        };
        var response = await _client.PostAsJsonAsync("http://localhost:8080/api/ClientCard", clientDto);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ClientCardDto>();
    }
}