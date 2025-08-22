using Application.Models.Requests;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> LoginAsync(LoginRequestDto request);
    }
}
