using Domain.Models;

namespace Infrastructure.Repositories.UserRepositories
{
    public class RequesterRepository<T> : UserRepository<T> where T : Requester
    {
        public RequesterRepository(DonationsDbContext dbContext) : base(dbContext)
        {

        }
    }
}
