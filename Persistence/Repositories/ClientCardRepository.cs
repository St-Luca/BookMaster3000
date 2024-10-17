using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Interfaces;

namespace Persistence.Repositories;

public class ClientCardRepository(LibraryContext _context) : IClientCardRepository
{
    public async Task<ClientCard?> GetClientCardById(int id)
    {
        return await _context.Clients.FindAsync(id);
    }

    public async Task<List<ClientCard>> GetAllClientCards()
    {
        return await _context.Clients.ToListAsync();
    }

    public async Task AddClientCard(ClientCard client)
    {
        _context.Clients.Add(client);
        await _context.SaveChangesAsync();
    }

    public async Task EditClientCard(ClientCard client)
    {
        _context.Clients.Update(client);
        await _context.SaveChangesAsync();
    }

    public async Task<ClientCard?> FindClientByPhoneOrEmail(string phone, string email)
    {
        return await _context.Clients
            .FirstOrDefaultAsync(c => c.Phone == phone || c.Email == email);
    }
}