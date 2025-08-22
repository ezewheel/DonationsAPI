using Domain.Models;

namespace Domain.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        Task<Role?> GetAsync(string name);
    }
}
