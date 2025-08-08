using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class DonationsContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DonationsContext(DbContextOptions<DonationsContext> options) : base(options)
        {

        }
    }
}