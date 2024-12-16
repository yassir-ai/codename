using System.Data.Common;
using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using UserService.Controllers;
using UserService.Dtos;
using UserService.Extensions;
using UserService.Interfaces;
using UserService.Model;

namespace UserService.Tests.Controllers;

public class UserControllerTests
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserControllerTests()
    {
        _userRepository = A.Fake<IUserRepository>();
        _mapper = A.Fake<IMapper>();
    }

    [Fact]
    public async Task UserController_GetAllUsersAsync_ReturnOK()
    {
        //Arrange
        var users = A.Fake<IEnumerable<User>>();
        var usersDto = A.Fake<IEnumerable<UserDtoRead>>();

        //Configure Mocks
        A.CallTo(() => _mapper.Map<IEnumerable<UserDtoRead>>(users)).Returns(usersDto);

        var controller = new UserController(_userRepository, _mapper);

        //Act
        var result = await controller.GetAllUsersAsync();

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>().Which.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task UserController_CreateUserAsync_ReturnCreatedAtAction()
    {
        //Arrange
        var userDto = A.Fake<UserDtoCreate>();
        var user = A.Fake<User>();
        
        //Configure Mocks
        A.CallTo(() => _mapper.Map<User>(userDto)).Returns(user);
        A.CallTo(() => _userRepository.CreateUserAsync(user)).Returns(Task.CompletedTask);

        var controller = new UserController(_userRepository, _mapper);

        //Act
        var result = await controller.CreateUserAsync(userDto);

        //Assert
        result.Should().BeOfType<CreatedAtActionResult>().Which.StatusCode.Should().Be(201);

        //Check Mocks
        A.CallTo(() => _userRepository.CreateUserAsync(user)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task UserController_GetUserAsync_ReturnOk()
    {
        //Arrange
        var userDto = A.Fake<UserDtoRead>();
        var user = A.Fake<User>();
        var id = Guid.NewGuid().ToString();
        user.Id = id;

        //Configure Mocks
        A.CallTo(() => _mapper.Map<UserDtoRead>(user)).Returns(userDto);
        A.CallTo(() => _userRepository.GetUserAsync(id)).Returns(user);

        var controller = new UserController(_userRepository, _mapper);

        //Act
        var result = await controller.GetUserAsync(id);

        //Assert
        result.Should().BeOfType<OkObjectResult>().Which.StatusCode.Should().Be(200);
        result.Should().BeOfType<OkObjectResult>().Which.Value.Should().Be(userDto);

        //Check Mocks
        A.CallTo(() => _userRepository.GetUserAsync(user.Id)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task UserController_UpdateUserAsync_ReturnNoContent()
    {
        //Arrange
        var userDto = A.Fake<UserDtoUpdate>();
        var user = A.Fake<User>();
        var id = Guid.NewGuid().ToString();
        user.Id = id;

        //Configure Mocks
        A.CallTo(() => _userRepository.GetUserAsync(id)).Returns(user);
        //A.CallTo(() => userDto.ToUserModel(user)).DoesNothing();
        A.CallTo(() => _userRepository.UpdateUserAsync(user)).Returns(Task.CompletedTask);

        var controller = new UserController(_userRepository, _mapper);

        //Act
        var result = await controller.UpdateUserAsync(user.Id, userDto);

        //Assert
        result.Should().BeOfType<NoContentResult>().Which.StatusCode.Should().Be(204);

        //Check Mocks
        A.CallTo(() => _userRepository.GetUserAsync(id)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _userRepository.UpdateUserAsync(user)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task Usercontroller_DeleteUser_ReturnNoContent()
    {
        //Arrange
        var user = A.Fake<User>();
        var id = Guid.NewGuid().ToString();
        user.Id = id;

        //Configure Mocks
        A.CallTo(() => _userRepository.GetUserAsync(id)).Returns(user);
        A.CallTo(() => _userRepository.DeleteUserAsync(user)).Returns(Task.CompletedTask);

        var controller = new UserController(_userRepository, _mapper);

        //Act
        var result = await controller.DeleteUser(id);

        //Assert
        result.Should().BeOfType<NoContentResult>().Which.StatusCode.Should().Be(204);

        //Check Mocks
        A.CallTo(() => _userRepository.GetUserAsync(id)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _userRepository.DeleteUserAsync(user)).MustHaveHappenedOnceExactly();
    }
}
