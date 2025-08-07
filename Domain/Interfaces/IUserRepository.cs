using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        User Get(string username);
    }
}
