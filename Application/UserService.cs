using Application.DTOs;
using Application.Requests;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Models;

namespace Application
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<UserForView> GetAllUsers()
        {
            var users = _userRepository.GetAll();

            return users.Select(u => new UserForView
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email,
                Role = u.Role,
                AccountStatus = u.AccountStatus,
                UserType = u is Donator ? "Donator" : u is Requester ? "Requester" : "Unknown"
            }).ToList();
        }

        public void AddDonator(DonatorForCreation donatorForCreation)
        {
            var exists = _userRepository.GetByUsername(donatorForCreation.Username);
            if (exists != null)
                throw new Exception("User already exists.");

            var donator = new Donator
            {
                Username = donatorForCreation.Username,
                Password = donatorForCreation.Password,
                Email = donatorForCreation.Email,
                Role = Role.Standard,
                DonationCount = 0
            };
            _userRepository.AddUser(donator);
        }

        public void AddRequester(RequesterForCreation requesterForCreation, Role creatorRole)
        {
            if (creatorRole != Role.Admin && creatorRole != Role.Moderator)
                throw new UnauthorizedAccessException("Only Admins or Moderators can create Requester accounts.");

            var exists = _userRepository.GetByUsername(requesterForCreation.Username);
            if (exists != null)
                throw new Exception("User already exists.");

            Requester requester = new Requester
            {
                Username = requesterForCreation.Username,
                Password = requesterForCreation.Password,
                Email = requesterForCreation.Email,
                Role = Role.Standard,
                OrganizationName = requesterForCreation.OrganizationName
            };
            _userRepository.AddUser(requester);
        }

        public User? ValidateUser(LoginRequest request)
        {
            User? userToReturn = _userRepository.GetByUsername(request.Username);
            if (userToReturn is not null && userToReturn.Password == request.Password)
                return userToReturn;
            return null;
        }
    }
}
