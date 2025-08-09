using Application.DTOs;
using Application.Requests;
using Domain.Enums;
using Domain.Models;

namespace Application
{
    public interface IUserService
    {
        List<UserForView> GetAllUsers();
        void AddDonator(DonatorForCreation donatorForCreation);
        void AddRequester(RequesterForCreation requesterForCreation, Role creatorRole);
        User? ValidateUser(LoginRequest request);
    }
}
