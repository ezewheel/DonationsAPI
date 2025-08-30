using Domain.Interfaces.Repositories;
using Domain.Models;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.UserRepositories
{
    public class UserRepository<T> : BaseRepository<T>, IUserRepository<T> where T : User
    {
        public UserRepository(DonationsDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<T?> GetAsync(string email)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
