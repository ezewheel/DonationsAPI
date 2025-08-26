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
            Donator admin = new Donator()
            {
                Id = 1,
                Name = "admin",
                Email = "admin",
                Password = BCrypt.Net.BCrypt.HashPassword("admin"),
                Role = Role.Admin
            };

            modelBuilder.Entity<Donator>().HasData(admin);

            modelBuilder.Entity<User>()
                .HasDiscriminator<string>("UserType")
                .HasValue<Donator>("Donator")
                .HasValue<Requester>("Requester");


            base.OnModelCreating(modelBuilder);
        }
    }
}