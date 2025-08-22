using Domain.Models;

namespace Domain.Interfaces.Repositories
{
    public interface IUserRepository<T> : IBaseRepository<T> where T : User
    {
        Task<T?> GetAsync(string email);

    }
}
