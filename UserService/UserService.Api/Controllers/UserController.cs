using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UserService.Dtos;
using UserService.Extensions;
using UserService.Interfaces;
using UserService.Model;

namespace UserService.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UserController> _logger; 

    public UserController(IUserRepository userRepository, IMapper mapper, ILogger<UserController> logger)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUserAsync([FromBody] UserDtoCreate userCreateDto)
    {
        var user = _mapper.Map<User>(userCreateDto);

        await _userRepository.CreateUserAsync(user);

        _logger.LogInformation("User created successfully with UserId {UserId} at {Time}", user.Id, user.CreatedAt);

        return Created();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllUsersAsync();

        var usersDtos = _mapper.Map<IEnumerable<UserDtoRead>>(users);

        _logger.LogInformation("Fetched {UserCount} users at {Time}", usersDtos.Count(), DateTime.Now);

        return Ok(usersDtos);   
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserAsync([FromRoute]string id)
    {
        var user = await _userRepository.GetUserAsync(id);

        var userDto = _mapper.Map<UserDtoRead>(user);

        _logger.LogInformation("Fetched user with ID {UserId} at {Time}", id, DateTime.Now);

        return Ok(userDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUserAsync([FromRoute] string id, [FromBody]UserDtoUpdate userUpdateDto)
    {
        var user = await _userRepository.GetUserAsync(id);

        userUpdateDto.ToUserModel(user);

        await _userRepository.UpdateUserAsync(user);

        _logger.LogInformation("User with ID {UserId} updated successfully at {Time}", id, DateTime.Now);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser([FromRoute]string id)
    {
        var user = await _userRepository.GetUserAsync(id);

        await _userRepository.DeleteUserAsync(user);

        _logger.LogInformation("User with ID {UserId} deleted successfully at {Time}", id, DateTime.Now);

        return NoContent();
    }
}
