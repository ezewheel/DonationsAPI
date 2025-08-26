using Application.Models.Requests;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> LoginAsync(LoginRequestDto request);
        Task<string> RegisterDonatorAsync(DonatorRegistrationRequestDto request);
        Task<string> RegisterRequesterAsync(RequesterRegistrationRequestDto request);
    }
}
