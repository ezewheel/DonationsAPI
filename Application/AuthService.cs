using Application.Interfaces;
using Application.Models.Requests;
using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Application
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository<User> _userRepository;

        public AuthService(IUserRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> LoginAsync(LoginRequestDto request)
        {
            User? user = await _userRepository.GetAsync(request.Email);
            if (user != null && BCrypt.Net.BCrypt.Verify(user.Password, request.Password))
            {
                return "Hello world";
            }
            return "Goodbye cruel world";
        }
    }
}
