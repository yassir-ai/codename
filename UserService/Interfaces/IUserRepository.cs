using UserService.Model;

namespace UserService.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User> GetUserAsync(string id);
    Task<bool> UserExistsAsync(string id);
    Task CreateUserAsync(User user);
    Task SaveAsync();
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(User user);
}