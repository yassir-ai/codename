using UserService.Data;
using UserService.Exceptions;
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

    public void CreateUser(User user)
    {
        _dbContext.Add(user);
        Save();
    }

    public IEnumerable<User> GetAllUsers()
    {
        var users = _dbContext.Users.AsEnumerable();
        return users;
    }

    public User GetUser(string id)
    {
        var user = _dbContext.Users.Where(u => u.Id == id).FirstOrDefault();

        if (user == null)
        {
            throw new EntityNotFoundException($"User with ID {id} not found.");
        }

        return user;
    }

    public void Save()
    {
        try
        {
            _dbContext.SaveChanges();
        }
        catch (Exception ex)
        {
            throw new DatabaseSavingException("An error occurred while saving changes to the database.", ex);
        }
    }

    public bool UserExists(string id)
    {
        return _dbContext.Users.Any(u => u.Id == id);
    }

    public void UpdateUser(User user)
    {
        _dbContext.Update(user);
        Save();
    }

    public void DeleteUser(User user)
    {
        _dbContext.Remove(user);
        Save();
    }
}
