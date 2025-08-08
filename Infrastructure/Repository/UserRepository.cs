using Domain.Interfaces;
using Domain.Models;

namespace Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DonationsContext _donationsContext;
        public UserRepository(DonationsContext donationsContext)
        {
            _donationsContext = donationsContext;
        }

        public List<User> GetAll()
        {
            return _donationsContext.Users.ToList();
        }

        public User? GetById(int id)
        {
            return _donationsContext.Users.FirstOrDefault(u => u.Id == id && u.AccountStatus == true);
        }

        public List<Donator> GetDonators()
        {
            return _donationsContext.Users.OfType<Donator>().Where(d => d.AccountStatus == true).ToList();
        }

        public List<Requester> GetRequesters()
        {
            return _donationsContext.Users.OfType<Requester>().Where(r => r.AccountStatus == true).ToList();
        }

        public void AddUser(Donator donator)
        {
            _donationsContext.Users.Add(donator);
            _donationsContext.SaveChanges();
        }

        public void AddUser(Requester requester)
        {
            _donationsContext.Users.Add(requester);
            _donationsContext.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            _donationsContext.Users.Update(user);
            _donationsContext.SaveChanges();
        }

        public void UpdateUser(Donator donator)
        {
            _donationsContext.Users.Update(donator);
            _donationsContext.SaveChanges();
        }
        public void UpdateUser(Requester requester)
        {
            _donationsContext.Users.Update(requester);
            _donationsContext.SaveChanges();
        }
    }
}
