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

        public async Task<ClientDto> FindClientById(string id)
        {
            var client = await _clientRepository.GetClientById(id);
            if (client == null) return null;

            return new ClientDto
            {
                Id = client.Id,
                Name = client.Name,
                Email = client.Email,
                Phone = client.Phone,
                Address = client.Address,
                City = client.City,
                Zip = client.Zip
            };
        }

        public async Task<List<ClientDto>> FindClientsByName(string name)
        {
            var clients = await _clientRepository.GetAllClients();
            var filteredClients = clients
                .Where(c => c.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                .Select(c => new ClientDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Email = c.Email,
                    Phone = c.Phone,
                    Address = c.Address,
                    City = c.City,
                    Zip = c.Zip
                }).ToList();

            return filteredClients;
        }

        public async Task<(bool IsSuccess, string Message, ClientDto Client)> AddClient(ClientDto clientData)
        {
            var existingClient = await _clientRepository.FindClientByPhoneOrEmail(clientData.Phone, clientData.Email);
            if (existingClient != null)
                return (false, "Client with this phone or email already exists", null);

            var newClient = new Client
            {
                Name = clientData.Name,
                Email = clientData.Email,
                Phone = clientData.Phone,
                Address = clientData.Address,
                City = clientData.City,
                Zip = clientData.Zip
            };

            await _clientRepository.AddClient(newClient);

            var clientDto = new ClientDto
            {
                Id = newClient.Id,
                Name = newClient.Name,
                Email = newClient.Email,
                Phone = newClient.Phone,
                Address = newClient.Address,
                City = newClient.City,
                Zip = newClient.Zip
            };

            return (true, null, clientDto);
        }

        public async Task<(bool IsSuccess, string Message, ClientDto Client)> EditClient(string id, ClientDto clientData)
        {
            var client = await _clientRepository.GetClientById(id);
            if (client == null)
                return (false, "Client not found", null);

            client.Name = clientData.Name;
            client.Email = clientData.Email;
            client.Phone = clientData.Phone;
            client.Address = clientData.Address;
            client.City = clientData.City;
            client.Zip = clientData.Zip;

            await _clientRepository.EditClient(client);

            var clientDto = new ClientDto
            {
                Id = client.Id,
                Name = client.Name,
                Email = client.Email,
                Phone = client.Phone,
                Address = client.Address,
                City = client.City,
                Zip = client.Zip
            };

            return (true, null, clientDto);
        }
    }
}

