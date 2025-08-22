using Domain.Models;

namespace Infrastructure.Repositories.UserRepositories
{
    public class DonatorRepository<T> : UserRepository<T> where T : Donator
    {
        public DonatorRepository(DonationsDbContext dbContext) : base(dbContext)
        {

        }
    }
}
