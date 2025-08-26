using Domain.Models;

namespace Infrastructure.Repositories.UserRepositories
{
    public class DonatorRepository : UserRepository<Donator>
    {
        public DonatorRepository(DonationsDbContext dbContext) : base(dbContext)
        {

        }
    }
}
