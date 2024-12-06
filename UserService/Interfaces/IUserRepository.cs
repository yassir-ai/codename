using UserService.Model;

namespace UserService.Interfaces;

public interface IUserRepository
{
    bool CreateUser(User user);
    bool Save();
}