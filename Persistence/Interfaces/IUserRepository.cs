using Domain.Entities;

namespace Persistence.interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserByUsername(string username);
        Task AddUser(User user);
        Task<bool> UserExists(string username);
    }
}