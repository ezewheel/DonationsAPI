using Domain.Models;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        List<User> GetAll();
        User? GetById(int id);
        List<Donator> GetDonators();
        List<Requester> GetRequesters();
        void AddUser(Donator donator);
        void AddUser(Requester requester);
        void UpdateUser(User user);
        void UpdateUser(Donator donator);
        void UpdateUser(Requester requester);
    }
}
