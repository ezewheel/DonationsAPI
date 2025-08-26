using Domain.Interfaces.Repositories;

namespace Infrastructure.Repository
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        public readonly DonationsDbContext _dbContext;

        public BaseRepository(DonationsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(T element)
        {
            await _dbContext.Set<T>().AddAsync(element);
            await _dbContext.SaveChangesAsync();
        }
    }
}