using Xunit;
using Moq;
using Application.Services;
using Application.Interfaces;
using Domain.Entities;
using Persistence.Interfaces;
using Application.Dto;
using System.Collections.Generic;
using System.Linq;

namespace BookMaster.Tests
{
    public class ClientCardServiceTests
    {
        private readonly Mock<IClientCardRepository> _mockClientRepository;
        private readonly Mock<IBookService> _mockBookService;
        private readonly ClientCardService _clientService;

        public ClientCardServiceTests()
        {
            _mockClientRepository = new Mock<IClientCardRepository>();
            _mockBookService = new Mock<IBookService>();
            _clientService = new ClientCardService(_mockClientRepository.Object, _mockBookService.Object);
        }

        [Fact]
        public async Task AddClient_ReturnsSuccess_WhenClientIsAdded()
        {
            // Arrange
            var clientDto = new ClientCardDto { Name = "John Doe" };
            _mockClientRepository.Setup(repo => repo.AddClientCard(It.IsAny<ClientCard>())).Returns(Task.CompletedTask);

            // Act
            var result = await _clientService.AddClient(clientDto);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("", result.Message);
            Assert.Equal(clientDto.Name, result.Client.Name);
        }

        [Fact]
        public async Task AddClient_ReturnsFailure_WhenClientDataIsInvalid()
        {
            // Arrange
            var clientDto = new ClientCardDto { Name = "" }; // Invalid name
            _mockClientRepository.Setup(repo => repo.AddClientCard(It.IsAny<ClientCard>())).Returns(Task.CompletedTask);

            // Act
            var result = await _clientService.AddClient(clientDto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Invalid client data", result.Message);
        }

        [Fact]
        public async Task FindClientById_ReturnsClient_WhenClientExists()
        {
            // Arrange
            var client = new ClientCard { Id = 1, Name = "John Doe" };
            _mockClientRepository.Setup(repo => repo.GetClientCardById(1)).ReturnsAsync(client);

            // Act
            var result = await _clientService.FindClientById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(client.Name, result.Name);
        }

        [Fact]
        public async Task FindClientById_ReturnsNull_WhenClientDoesNotExist()
        {
            // Arrange
            _mockClientRepository.Setup(repo => repo.GetClientCardById(1)).ReturnsAsync((ClientCard)null);

            // Act
            var result = await _clientService.FindClientById(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task EditClient_UpdatesClient_WhenClientExists()
        {
            // Arrange
            var client = new ClientCard { Id = 1, Name = "John Doe" };
            var clientDto = new ClientCardDto { Name = "Jane Doe" };

            _mockClientRepository.Setup(repo => repo.GetClientCardById(1)).ReturnsAsync(client);
            _mockClientRepository.Setup(repo => repo.EditClientCard(It.IsAny<ClientCard>())).Returns(Task.CompletedTask);

            // Act
            var result = await _clientService.EditClient(1, clientDto);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("", result.Message);
            Assert.Equal("Jane Doe", client.Name);
        }

        [Fact]
        public async Task EditClient_ReturnsFailure_WhenClientDoesNotExist()
        {
            // Arrange
            var clientDto = new ClientCardDto { Name = "Jane Doe" };
            _mockClientRepository.Setup(repo => repo.GetClientCardById(1)).ReturnsAsync((ClientCard)null);

            // Act
            var result = await _clientService.EditClient(1, clientDto);

            // Assert

            Assert.False(result.IsSuccess);
            Assert.Equal("Client not found", result.Message);
        }

        [Fact]
        public async Task IssueBook_ReturnsSuccess_WhenBookIsIssued()
        {
            // Arrange
            var client = new ClientCard { Id = 1, Issues = new List<Issue>() };
            var book = new Book { Id = 1, Title = "Test Book" };

            _mockClientRepository.Setup(repo => repo.GetClientCardById(1)).ReturnsAsync(client);
            _mockBookService.Setup(service => service.GetBook(1)).ReturnsAsync(new BookDto { Id = 1, Title = "Test Book" });

            // Act
            var result = await _clientService.IssueBook(1, 1);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("", result.Message);
            Assert.Contains(client.Issues, i => i.BookId == 1);
        }

        [Fact]
        public async Task IssueBook_ReturnsFailure_WhenClientHasReachedMaxIssues()
        {
            // Arrange
            var client = new ClientCard { Id = 1, Issues = Enumerable.Range(1, 5).Select(i => new Issue { BookId = i }).ToList() };

            _mockClientRepository.Setup(repo => repo.GetClientCardById(1)).ReturnsAsync(client);

            // Act
            var result = await _clientService.IssueBook(1, 3);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Client card or book not found, or issues are filled", result.Message);
        }

        [Fact]
        public async Task IssueBook_ReturnsFailure_WhenClientCanReachMaxIssues()
        {
            // Arrange
            var client = new ClientCard { Id = 1, Issues = Enumerable.Range(1, 4).Select(i => new Issue { BookId = i }).ToList() };

            _mockClientRepository.Setup(repo => repo.GetClientCardById(1)).ReturnsAsync(client);

            // Act
            var result = await _clientService.IssueBook(1, 3);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Client card or book not found, or issues are filled", result.Message);
        }

        [Fact]
        public async Task RenewBook_ReturnsSuccess_WhenBookIsRenewed()
        {
            // Arrange
            var issue = new Issue { BookId = 1, IssueTo = DateTime.Now.AddDays(10), IsRenewed = false };
            var client = new ClientCard { Id = 1, Issues = new List<Issue> { issue } };

            _mockClientRepository.Setup(repo => repo.GetClientCardById(1)).ReturnsAsync(client);

            // Act
            var result = await _clientService.RenewBook(1, 1);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.True(issue.IsRenewed);
            Assert.Equal("", result.Message);
        }

        [Fact]
        public async Task RenewBook_ReturnsFailure_WhenBookAlreadyRenewed()
        {
            // Arrange
            var issue = new Issue { BookId = 1, IsRenewed = true };
            var client = new ClientCard { Id = 1, Issues = new List<Issue> { issue } };

            _mockClientRepository.Setup(repo => repo.GetClientCardById(1)).ReturnsAsync(client);

            // Act
            var result = await _clientService.RenewBook(1, 1);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Book has already been renewed", result.Message);
        }

        [Fact]
        public async Task ReturnBook_ReturnsSuccess_WhenBookIsReturnedOnTime()
        {
            // Arrange
            var issue = new Issue { BookId = 1, IssueTo = DateTime.Now.AddDays(1) };
            var client = new ClientCard { Id = 1, Issues = new List<Issue> { issue }, Returns = new List<Issue>() };

            _mockClientRepository.Setup(repo => repo.GetClientCardById(1)).ReturnsAsync(client);

            // Act
            var result = await _clientService.ReturnBook(1, 1);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(string.Empty, result.Message);
            Assert.Single(client.Returns, i => i.BookId == 1);
        }

        [Fact]
        public async Task ReturnBook_ReturnsLateReturnMessage_WhenBookIsReturnedLate()
        {
            // Arrange
            var issue = new Issue { BookId = 1, IssueTo = DateTime.Now.AddDays(-1) }; // Late return
            var client = new ClientCard { Id = 1, Issues = new List<Issue> { issue }, Returns = new List<Issue>() };

            _mockClientRepository.Setup(repo => repo.GetClientCardById(1)).ReturnsAsync(client);

            // Act
            var result = await _clientService.ReturnBook(1, 1);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Too late return", result.Message);
            Assert.Single(client.Returns, i => i.BookId == 1);
        }

        [Fact]
        public async Task ReturnBook_ReturnsFailure_WhenClientOrIssueNotFound()
        {
            // Arrange: Client does not exist
            _mockClientRepository.Setup(repo => repo.GetClientCardById(1)).ReturnsAsync((ClientCard)null);

            // Act
            var result = await _clientService.ReturnBook(1, 1);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Client card or issue not found", result.Message);
        }

        [Fact]
        public async Task ReturnBook_ReturnsFailure_WhenIssueNotFoundForClient()
        {
            // Arrange: Client exists but has no issues for the book
            var client = new ClientCard { Id = 1, Issues = new List<Issue>(), Returns = new List<Issue>() };
            _mockClientRepository.Setup(repo => repo.GetClientCardById(1)).ReturnsAsync(client);

            // Act
            var result = await _clientService.ReturnBook(1, 1);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Client card or issue not found", result.Message);
        }

        [Fact]
        public async Task FindClientsByName_ReturnsClients_WhenClientsExistWithName()
        {
            // Arrange
            var clients = new List<ClientCard>
            {
                new ClientCard { Id = 1, Name = "John Doe" },
                new ClientCard { Id = 2, Name = "Jane Doe" }
            };

            _mockClientRepository.Setup(repo => repo.GetAllClientCards()).ReturnsAsync(clients);

            // Act
            var result = await _clientService.FindClientsByName("Doe");

            // Assert
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task FindClientsByName_ReturnsPartialMatches_WhenClientsExistWithPartialName()
        {
            // Arrange
            var clients = new List<ClientCard>
            {
                new ClientCard { Id = 1, Name = "John Doe" },
                new ClientCard { Id = 2, Name = "Jane Smith" }
            };

            _mockClientRepository.Setup(repo => repo.GetAllClientCards()).ReturnsAsync(clients);

            // Act
            var result = await _clientService.FindClientsByName("Jo");

            // Assert
            Assert.Single(result);
            Assert.Equal("John Doe", result[0].Name);
        }

        [Fact]
        public async Task FindClientsByName_ReturnsEmpty_WhenNoClientsMatchName()
        {
            // Arrange
            var clients = new List<ClientCard>
            {
                new ClientCard { Id = 1, Name = "John Doe" },
                new ClientCard { Id = 2, Name = "Jane Smith" }
            };

            _mockClientRepository.Setup(repo => repo.GetAllClientCards()).ReturnsAsync(clients);

            // Act
            var result = await _clientService.FindClientsByName("Nonexistent");

            // Assert
            Assert.Empty(result);
        }
    }
}
