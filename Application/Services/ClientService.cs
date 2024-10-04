using Application.interfaces;
using Application.Dto;
using Domain.Entities;
using Persistence.interfaces;

namespace Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<IEnumerable<ClientDto>> GetAllClientsAsync()
        {
            var clients = await _clientRepository.GetAllClientsAsync();
            return clients.Select(MapToDto);
        }

        public async Task<ClientDto> GetClientByIdAsync(int id)
        {
            var client = await _clientRepository.GetClientByIdAsync(id);
            return client == null ? null : MapToDto(client);
        }

        public async Task<ClientDto> CreateClientAsync(ClientDto clientDto)
        {
            var client = MapToEntity(clientDto);
            var createdClient = await _clientRepository.CreateClientAsync(client);
            return MapToDto(createdClient);
        }

        public async Task<ClientDto> UpdateClientAsync(int id, ClientDto clientDto)
        {
            var client = MapToEntity(clientDto);
            var updatedClient = await _clientRepository.UpdateClientAsync(id, client);
            return updatedClient == null ? null : MapToDto(updatedClient);
        }

        public async Task<bool> DeleteClientAsync(int id)
        {
            return await _clientRepository.DeleteClientAsync(id);
        }

        private ClientDto MapToDto(Client client)
        {
            return new ClientDto
            {
                Id = client.Id,
                Name = client.Name,
                Zip = client.Zip,
                City = client.City,
                Phone = client.Phone,
                Email = client.Email
            };
        }

        private Client MapToEntity(ClientDto clientDto)
        {
            return new Client
            {
                Id = clientDto.Id,
                Name = clientDto.Name,
                Zip = clientDto.Zip,
                City = clientDto.City,
                Phone = clientDto.Phone,
                Email = clientDto.Email
            };
        }
    }
}

