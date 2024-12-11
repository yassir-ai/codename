using Microsoft.EntityFrameworkCore;
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

    public async Task CreateUserAsync(User user)
    {
        await _dbContext.AddAsync(user);
        await SaveAsync();
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        var users = await _dbContext.Users.ToListAsync();
        return users;
    }

    public async Task<User> GetUserAsync(string id)
    {
        var user = await _dbContext.Users.Where(u => u.Id == id).FirstOrDefaultAsync();

        if (user == null)
        {
            throw new EntityNotFoundException($"User with ID {id} not found.");
        }

        return user;
    }

    public async Task SaveAsync()
    {
        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new DatabaseSavingException("An error occurred while saving changes to the database.", ex);
        }
    }

    public async Task<bool> UserExistsAsync(string id)
    {
        return await _dbContext.Users.AnyAsync(u => u.Id == id);
    }

    public async Task UpdateUserAsync(User user)
    {
        _dbContext.Update(user);
        await SaveAsync();
    }

    public async Task DeleteUserAsync(User user)
    {
        _dbContext.Remove(user);
        await SaveAsync();
    }
}
