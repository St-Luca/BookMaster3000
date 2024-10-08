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

    public async Task<Client> GetClientById(int id)
    {
        return await _context.Clients.FindAsync(id);
    }

    public async Task<List<Client>> GetAllClients()
    {
        return await _context.Clients.ToListAsync();
    }

    public async Task AddClient(Client client)
    {
        _context.Clients.Add(client);
        await _context.SaveChangesAsync();
    }

    public async Task EditClient(Client client)
    {
        _context.Clients.Update(client);
        await _context.SaveChangesAsync();
    }

    public async Task<Client> FindClientByPhoneOrEmail(string phone, string email)
    {
        return await _context.Clients
            .FirstOrDefaultAsync(c => c.Phone == phone || c.Email == email);
    }
}
}