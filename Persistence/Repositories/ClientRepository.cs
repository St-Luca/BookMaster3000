using Domain.Entities;
using Persistence.interfaces;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class ClientRepository : IClientRepository
{
    private readonly LibraryContext _context;

    public ClientRepository(LibraryContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Client>> GetAllClientsAsync()
    {
        return await _context.Clients.ToListAsync();
    }

    public async Task<Client> GetClientByIdAsync(int id)
    {
        return await _context.Clients.FindAsync(id);
    }

    public async Task<Client> CreateClientAsync(Client client)
    {
        _context.Clients.Add(client);
        await _context.SaveChangesAsync();
        return client;
    }

    public async Task<Client> UpdateClientAsync(int id, Client client)
    {
        var existingClient = await _context.Clients.FindAsync(id);
        if (existingClient == null) return null;

        existingClient.Name = client.Name;
        existingClient.Zip = client.Zip;
        existingClient.City = client.City;
        existingClient.Phone = client.Phone;
        existingClient.Email = client.Email;

        await _context.SaveChangesAsync();
        return existingClient;
    }

    public async Task<bool> DeleteClientAsync(int id)
    {
        var client = await _context.Clients.FindAsync(id);
        if (client == null) return false;

        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();
        return true;
    }
}
}