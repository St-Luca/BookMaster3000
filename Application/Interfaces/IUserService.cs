using Domain.Entities;
namespace Application.interfaces
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);

    }
}