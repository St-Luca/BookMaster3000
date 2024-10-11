using Xunit;
using Moq;
using Application.Services;
using Domain.Entities;
using Persistence.interfaces;
using Application.Dto;
using System.Collections.Generic;
using System.Linq;
namespace BookMaster.Tests
{
    public class ClientServiceTests
    {
        public ClientServiceTests(){}

        [Fact]
        public async void AddClient_ReturnsClientDto()
        {
            var mockClientRepository = new Mock<IClientRepository>();
            var ClientService = new ClientService(mockClientRepository.Object);

            var ClientDto = new ClientDto { Name = "John Doe", Email = "john@example.com", Phone = "1234567890" };
            var Client = new Client { Name = "John Doe", Email = "john@example.com", Phone = "1234567890" };

            mockClientRepository.Setup(repo => repo.AddClient(It.IsAny<Client>())).Callback<Client>(u => Client = u);

            var result = await ClientService.AddClient(ClientDto);

            Assert.Equal(ClientDto.Name, result.Client.Name);
            Assert.Equal(ClientDto.Email, result.Client.Email);
            Assert.Equal(ClientDto.Phone, result.Client.Phone);
            mockClientRepository.Verify(repo => repo.AddClient(It.IsAny<Client>()), Times.Once);
        }


        [Fact]
        public async void UpdateClient_UpdatesExistingClient()
        {
            var mockClientRepository = new Mock<IClientRepository>();
            var ClientService = new ClientService(mockClientRepository.Object);

            var existingClient = new Client { Id = 1, Name = "John Doe", Email = "john@example.com", Phone = "1234567890" };
            var updatedClientDto = new ClientDto { Name = "John Smith", Email = "john.smith@example.com", Phone = "0987654321" };

            mockClientRepository.Setup(repo => repo.GetClientById(It.IsAny<int>())).Returns(Task.FromResult(existingClient));

            await ClientService.EditClient(1, updatedClientDto);

            Assert.Equal(updatedClientDto.Name, existingClient.Name);
            Assert.Equal(updatedClientDto.Email, existingClient.Email);
            Assert.Equal(updatedClientDto.Phone, existingClient.Phone);
            mockClientRepository.Verify(repo => repo.EditClient(It.IsAny<Client>()), Times.Once);
        }

        [Fact]
        public async void GetClientById_ReturnsClientDto()
        {
            var mockClientRepository = new Mock<IClientRepository>();
            var ClientService = new ClientService(mockClientRepository.Object);

            var Client = new Client { Id = 1, Name = "John Doe", Email = "john@example.com", Phone = "1234567890" };

            mockClientRepository.Setup(repo => repo.GetClientById(It.IsAny<int>())).Returns(Task.FromResult((Client)null));

            var result = await ClientService.FindClientById(1);

            Assert.Equal(Client.Name, result.Name);
            Assert.Equal(Client.Email, result.Email);
            Assert.Equal(Client.Phone, result.Phone);
        }

        [Fact]
        public async void AddClient_WithEmptyName_ThrowsArgumentException()
        {
            var mockClientRepository = new Mock<IClientRepository>();
            var ClientService = new ClientService(mockClientRepository.Object);

            var ClientDto = new ClientDto { Name = "", Email = "john@example.com", Phone = "1234567890" };

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await ClientService.AddClient(ClientDto));
            Assert.Equal("Name cannot be empty", exception.Message);
            mockClientRepository.Verify(repo => repo.AddClient(It.IsAny<Client>()), Times.Never);
        }

        [Fact]
        public async void UpdateClient_ClientNotFound_ThrowsException()
        {

            var mockClientRepository = new Mock<IClientRepository>();
            var ClientService = new ClientService(mockClientRepository.Object);
            var existingClient = new Client { Id = 1, Name = "John Doe", Email = "john@example.com", Phone = "1234567890" };
            var updatedClientDto = new ClientDto { Name = "John Smith", Email = "john.smith@example.com", Phone = "0987654321" };

            mockClientRepository.Setup(repo => repo.GetClientById(It.IsAny<int>())).Returns(Task.FromResult(existingClient));

            var exception = await Assert.ThrowsAsync<Exception>(() => ClientService.EditClient(1, updatedClientDto));
            Assert.Equal("Client not found", exception.Message);
            mockClientRepository.Verify(repo => repo.EditClient(It.IsAny<Client>()), Times.Never);
        }

        [Fact]
        public async void GetClientById_ClientExists_ReturnsClientDto()
        {
            var mockClientRepository = new Mock<IClientRepository>();
            var ClientService = new ClientService(mockClientRepository.Object);

            var Client = new Client { Id = 1, Name = "John Doe", Email = "john@example.com", Phone = "1234567890" };

            mockClientRepository.Setup(repo => repo.GetClientById(1)).Returns(Task.FromResult(Client));

            var result = await ClientService.FindClientById(1);

            Assert.NotNull(result);
            Assert.Equal(Client.Name, result.Name);
            Assert.Equal(Client.Email, result.Email);
            Assert.Equal(Client.Phone, result.Phone);
            mockClientRepository.Verify(repo => repo.GetClientById(1), Times.Once);
        }

        [Fact]
        public async void GetClientById_ClientNotFound_ThrowsException()
        {
            var mockClientRepository = new Mock<IClientRepository>();
            var ClientService = new ClientService(mockClientRepository.Object);
            var existingClient = new Client { Id = 1, Name = "John Doe", Email = "john@example.com", Phone = "1234567890" };
            mockClientRepository.Setup(repo => repo.GetClientById(It.IsAny<int>())).Returns(Task.FromResult(existingClient));

            var exception = await Assert.ThrowsAsync<Exception>(() => ClientService.FindClientById(1));
            Assert.Equal("Client not found", exception.Message);
            mockClientRepository.Verify(repo => repo.GetClientById(1), Times.Once);
        }

        [Fact]
        public async void UpdateClient_ValidClient_UpdatesCorrectly()
        {
            var mockClientRepository = new Mock<IClientRepository>();
            var ClientService = new ClientService(mockClientRepository.Object);

            var existingClient = new Client { Id = 1, Name = "John Doe", Email = "john@example.com", Phone = "1234567890" };
            var updatedClientDto = new ClientDto { Name = "John Smith", Email = "john.smith@example.com", Phone = "0987654321" };

            mockClientRepository.Setup(repo => repo.GetClientById(It.IsAny<int>())).Returns(Task.FromResult(existingClient));

            await ClientService.EditClient(1, updatedClientDto);

            Assert.Equal(updatedClientDto.Name, existingClient.Name);
            Assert.Equal(updatedClientDto.Email, existingClient.Email);
            Assert.Equal(updatedClientDto.Phone, existingClient.Phone);
            mockClientRepository.Verify(repo => repo.EditClient(It.IsAny<Client>()), Times.Once);
        }

        [Fact]
        public async void AddClient_WithUniqueEmail_Successful()
        {
            var mockClientRepository = new Mock<IClientRepository>();
            var ClientService = new ClientService(mockClientRepository.Object);

            var ClientDto = new ClientDto { Name = "Jane Doe", Email = "jane@example.com", Phone = "1234567890" };
            mockClientRepository.Setup(repo => repo.FindClientByPhoneOrEmail("", It.IsAny<string>())).Returns(Task.FromResult((Client)null));

            var result = await ClientService.AddClient(ClientDto);

            Assert.NotNull(result);
            Assert.Equal(ClientDto.Email, result.Client.Email);
            mockClientRepository.Verify(repo => repo.AddClient(It.IsAny<Client>()), Times.Once);
        }

        [Fact]
        public async void AddClient_WithDuplicateEmail_ThrowsException()
        {
            var mockClientRepository = new Mock<IClientRepository>();
            var ClientService = new ClientService(mockClientRepository.Object);

            var existingClient = new Client { Id = 1, Name = "John Doe", Email = "john@example.com", Phone = "1234567890" };
            var ClientDto = new ClientDto { Name = "Jane Doe", Email = "john@example.com", Phone = "0987654321" };

            mockClientRepository.Setup(repo => repo.FindClientByPhoneOrEmail("", It.IsAny<string>())).Returns(Task.FromResult(existingClient));

            var exception = await Assert.ThrowsAsync<Exception>(() => ClientService.AddClient(ClientDto));
            Assert.Equal("Email already exists", exception.Message);
            mockClientRepository.Verify(repo => repo.AddClient(It.IsAny<Client>()), Times.Never);
        }

        [Fact]
        public async void UpdateClient_ChangesEmail_Successful()
        {
            var mockClientRepository = new Mock<IClientRepository>();
            var ClientService = new ClientService(mockClientRepository.Object);

            var existingClient = new Client { Id = 1, Name = "John Doe", Email = "john@example.com", Phone = "1234567890" };
            var updatedClientDto = new ClientDto { Name = "John Doe", Email = "john.new@example.com", Phone = "1234567890" };

            mockClientRepository.Setup(repo => repo.GetClientById(1)).Returns(Task.FromResult(existingClient));
            mockClientRepository.Setup(repo => repo.FindClientByPhoneOrEmail("","john.new@example.com")).Returns(Task.FromResult(existingClient));

            await ClientService.EditClient(1, updatedClientDto);

            Assert.Equal(updatedClientDto.Email, existingClient.Email);
            mockClientRepository.Verify(repo => repo.EditClient(It.IsAny<Client>()), Times.Once);
        }

    }
}
