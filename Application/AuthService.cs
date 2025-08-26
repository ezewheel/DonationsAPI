using Application.Interfaces;
using Application.Models.Requests;
using Domain.Interfaces.Repositories;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly IUserRepository<User> _userRepository;
        private readonly IRoleRepository _roleRepository;

        public AuthService(IConfiguration config, IUserRepository<User> userRepository, IRoleRepository roleRepository)
        {
            _config = config;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        private string GenerateJwtToken(User user)
        {
            var securityPassword = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Authentication:SecretForKey"]!));

            SigningCredentials signature = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim(ClaimTypes.Role, user.Role.Name));

            var jwtSecurityToken = new JwtSecurityToken(
                _config["Authentication:Issuer"],
                _config["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                signature);

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        public async Task<string> LoginAsync(LoginRequestDto request)
        {
            User? user = await _userRepository.GetAsync(request.Email);
            if (user != null && BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                return GenerateJwtToken(user);
            }

            throw new UnauthorizedAccessException("Invalid email or password.");
        }

        public async Task<string> RegisterAsync(RegistrationRequestDto request)
        {
            if (await _userRepository.GetAsync(request.Email) != null)
            {
                throw new InvalidOperationException("User already exists.");
            }

            Role? role = await _roleRepository.GetAsync(request.Role);
            if (role == null)
            {
                throw new InvalidOperationException("Invalid role.");
            }

            User user = new User
            {
                Name = request.Name,
                Email = request.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                RoleId = role.Id,
                Role = role
            };
            await _userRepository.AddAsync(user);
            return GenerateJwtToken(user);
        }
    }
}
