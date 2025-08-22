using Application.Interfaces;
using Application.Models.Requests;
using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Application
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository<User> _userRepository;
        private readonly IRoleRepository _roleRepository;

        public AuthService(IUserRepository<User> userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task<string> LoginAsync(LoginRequestDto request)
        {
            User? user = await _userRepository.GetAsync(request.Email);
            if (user != null && BCrypt.Net.BCrypt.Verify(user.Password, request.Password))
            {
                return "Login successful";
            }
            return "Login failed";
        }

        public async Task<string> RegisterAsync(RegistrationRequestDto request)
        {
            if (await _userRepository.GetAsync(request.Email) != null)
            {
                return "User already exists";
            }

            Role? role = await _roleRepository.GetAsync(request.Role);
            if (role == null)
            {
                return "Invalid role";
            }

            User user = new User
            {
                Name = request.Name,
                Email = request.Email,
                Password = request.Password,
                RoleId = role.Id
            };
            await _userRepository.AddAsync(user);
            return "Registration successful";
        }
    }
}
