using Domain.Entities;
namespace Application.interfaces
{
    public interface IUserService
    {
        Task<(bool IsSuccess, string Message, User? User)> Authenticate(string username, string password);

    }
}