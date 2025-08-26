using Domain.Models;

namespace Infrastructure.Repositories.UserRepositories
{
    public class RequesterRepository : UserRepository<Requester>
    {
        public RequesterRepository(DonationsDbContext dbContext) : base(dbContext)
        {

        }
    }
}
