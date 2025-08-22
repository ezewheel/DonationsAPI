using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class DonationsDbContext : DbContext
    {
        public DonationsDbContext(DbContextOptions<DonationsDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Role adminRole = new Role()
            {
                Id = 1,
                Name = "admin",
                Description = "System administrator"
            };

            User admin = new User()
            {
                Id = 1,
                Name = "admin",
                Email = "admin",
                Password = BCrypt.Net.BCrypt.HashPassword("admin"),
                RoleId = adminRole.Id
            };

            modelBuilder.Entity<Role>().HasData(adminRole);

            modelBuilder.Entity<User>().HasData(admin);

            modelBuilder.Entity<User>()
                .HasDiscriminator<string>("UserType")
                .HasValue<User>("User")
                .HasValue<Donator>("Donator")
                .HasValue<Requester>("Requester");


            base.OnModelCreating(modelBuilder);
        }
    }
}