using Domain.Entities;
using Application.interfaces;
using Persistence.interfaces;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<(bool IsSuccess, string Message, User? User)> Authenticate(string username, string password)
        {
            var user = await _userRepository.GetUserByUsername(username);
            
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
                return (false, $"Wrong authentication data", null);

            return (true, "Authentification is complete", user);
        }

    }
}