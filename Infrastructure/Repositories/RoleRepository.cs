using Domain.Interfaces.Repositories;
using Domain.Models;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(DonationsDbContext dbContext) : base(dbContext)
        {

        }
        public async Task<Role?> GetAsync(string name)
        {
            return await _dbContext.Set<Role>().FirstOrDefaultAsync(r => r.Name == name);
        }
    }
}
