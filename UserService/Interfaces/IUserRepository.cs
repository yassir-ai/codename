using UserService.Model;

namespace UserService.Interfaces;

public interface IUserRepository
{
    IEnumerable<User> GetAllUsers();
    User GetUser(string id);
    bool UserExists(string id);
    void CreateUser(User user);
    void Save();
    void UpdateUser(User user);
    void DeleteUser(User user);
}