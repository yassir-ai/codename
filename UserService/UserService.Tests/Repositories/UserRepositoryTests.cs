using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using UserService.Data;
using UserService.Model;
using UserService.Repository;

namespace UserService.Tests.Repositories;

public class UserRepositoryTests
{
    public DataContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

        return new DataContext(options);
    }

    [Fact]
    public async Task GetAllUsersAsync_UsersExist_ReturnsUsers()
    {
        //Arrange
        var context = GetInMemoryDbContext();
        var repository = new UserRepository(context);

        var user1 = new User { Id = Guid.NewGuid().ToString(), Name = Guid.NewGuid().ToString() };
        var user2 = new User { Id = Guid.NewGuid().ToString(), Name = Guid.NewGuid().ToString() };

        var users = new List<User>();
        users.Add(user1);
        users.Add(user2);
        
        await context.Users.AddRangeAsync(users);
        await context.SaveChangesAsync();

        //Act
        var result = await repository.GetAllUsersAsync();

        //Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result.Should().Contain(u => u.Id == user1.Id && u.Name == user1.Name);
        result.Should().Contain(u => u.Id == user2.Id && u.Name == user2.Name);
    }

    [Fact]
    public async Task GetUserAsync_UserExists_ReturnsUser()
    {
        //Arrange
        var context = GetInMemoryDbContext();
        var repository = new UserRepository(context);

        var id = Guid.NewGuid().ToString();
        var name = Guid.NewGuid().ToString();
        var user = new User { Id = id, Name = name};

        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();

        //Act
        var result = await repository.GetUserAsync(id);

        //Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(id);
        result.Name.Should().Be(name);
    }

    [Fact]
    public async Task CreateUserAsync_UserIsValid_AddsUserToDatabase()
    {
        //Arrange
        var context = GetInMemoryDbContext();
        var repository = new UserRepository(context);

        var id = Guid.NewGuid().ToString();
        var name = Guid.NewGuid().ToString();
        var user = new User { Id = id, Name = name};

        //Act
        await repository.CreateUserAsync(user);
        var userInDb = await context.Users.FindAsync(id);

        //Assert
        userInDb.Should().NotBeNull();
        userInDb!.Id.Should().Be(id);
        userInDb.Name.Should().Be(name);
    }

    [Fact]
    public async Task UpdateUserAsync_UserIsValid_UpdateUserInDatabase()
    {
        //Arrange
        var context = GetInMemoryDbContext();
        var repository = new UserRepository(context);

        var id = Guid.NewGuid().ToString();
        var newName = "new name";
        var oldUser = new User { Id = id, Name = "old name"};
        var updatedUser = new User { Id = id, Name = newName};

        await context.Users.AddAsync(oldUser);
        await context.SaveChangesAsync();

        //Act 
        await repository.UpdateUserAsync(updatedUser);
        var userInDb = await context.Users.FindAsync(id);

        //Assert
        userInDb.Should().NotBeNull();
        userInDb!.Name.Should().Be(newName);
    }

    [Fact]
    public async Task DeleteUserAsync_UserValid_DeleteUserInDatabase()
    {
        //Arrange
        var context = GetInMemoryDbContext();
        var repository = new UserRepository(context);

        var id = Guid.NewGuid().ToString();
        var user = new User { Id = id, Name = Guid.NewGuid().ToString() };

        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();

        //Act
        await repository.DeleteUserAsync(user);
        var result = await context.Users.FindAsync(id);

        //Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task SaveAsync_WhenSaveChangesAsyncSucceeds_ThrowsAnyException()
    {
        //Arrange
        var context = GetInMemoryDbContext();
        var repository = new UserRepository(context);

        //Act
        Func<Task> act = async () => await repository.SaveAsync();

        //Assert
        await act.Should().NotThrowAsync<Exception>();
    }

    [Fact]
    public async Task UserExistsAsync_UserExists_ReturnTrue()
    {
        //Arrange
        var context = GetInMemoryDbContext();
        var repository = new UserRepository(context);

        var id = Guid.NewGuid().ToString();
        var user = new User{ Id = id, Name = Guid.NewGuid().ToString()};

        await context.AddAsync(user);
        await context.SaveChangesAsync();

        //Act
        var result = await repository.UserExistsAsync(id);

        //Assert
        result.Should().Be(true);
    }
}
