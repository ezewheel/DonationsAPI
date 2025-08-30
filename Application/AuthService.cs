using Application.Interfaces;
using Application.Models.Requests;
using Domain.Interfaces.Repositories;
using Domain.Models;
using Domain.Models.Enums;
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

        public AuthService(IConfiguration config, IUserRepository<User> userRepository)
        {
            _config = config;
            _userRepository = userRepository;
        }

        private string GenerateJwtToken(User user)
        {
            var securityPassword = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Authentication:SecretForKey"]!));

            SigningCredentials signature = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>();

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
            if (user == null ||
                !BCrypt.Net.BCrypt.Verify(request.Password, user.Password) ||
                (user is Requester requester && requester.AdmissionStatus != AdmissionStatus.Accepted))
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            return GenerateJwtToken(user);
        }

        public async Task<string> RegisterDonatorAsync(DonatorRegistrationRequestDto request)
        {
            if (await _userRepository.GetAsync(request.Email) != null)
            {
                throw new InvalidOperationException("User already exists.");
            }

            Donator user = new Donator
            {
                Name = request.Name,
                Email = request.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = Role.Standard
            };
            await _userRepository.AddAsync(user);
            return GenerateJwtToken(user);
        }

        public async Task<string> RegisterRequesterAsync(RequesterRegistrationRequestDto request)
        {
            if (await _userRepository.GetAsync(request.Email) != null)
            {
                throw new InvalidOperationException("User already exists.");
            }

            Requester user = new Requester
            {
                Name = request.Name,
                Email = request.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = Role.Standard,
                AdmissionStatus = AdmissionStatus.Pending,
            };
            await _userRepository.AddAsync(user);
            return GenerateJwtToken(user);
        }
    }
}
