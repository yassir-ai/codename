using UserService.Data;
using UserService.Interfaces;
using UserService.Model;

namespace UserService.Repository;

public class UserRepository : IUserRepository
{
    private readonly DataContext _dbContext;

    public UserRepository(DataContext dbContext)
    {
        _dbContext = dbContext;
    }
    public bool CreateUser(User user)
    {
        _dbContext.Add(user);
        return Save();
    }

    public IEnumerable<User> GetAllUsers()
    {
        return _dbContext.Users.AsEnumerable();
    }

    public User GetUser(string id)
    {
        return _dbContext.Users.Where(u => u.Id == id).FirstOrDefault();
    }

    public bool Save()
    {
        return _dbContext.SaveChanges() > 0;
    }

    public bool UserExists(string id)
    {
        return _dbContext.Users.Any(u => u.Id == id);
    }

    public bool UpdateUser(User user)
    {
        _dbContext.Update(user);
        return Save();
    }

    public bool DeleteUser(User user)
    {
        _dbContext.Remove(user);
        return Save();
    }
}
