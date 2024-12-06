using UserService.Model;

namespace UserService.Interfaces;

public interface IUserRepository
{
    IEnumerable<User> GetAllUsers();
    User GetUser(string id);
    bool UserExists(string id);
    bool CreateUser(User user);
    bool Save();
}