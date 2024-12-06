using UserService.Data;
using UserService.Dto;
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

    public bool Save()
    {
        return _dbContext.SaveChanges() > 0;
    }
}
