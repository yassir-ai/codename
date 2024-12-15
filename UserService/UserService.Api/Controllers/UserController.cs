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

    public UserController(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUserAsync([FromBody] UserDtoCreate userCreateDto)
    {
        var user = _mapper.Map<User>(userCreateDto);

        await _userRepository.CreateUserAsync(user);

        return Created();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllUsersAsync();

        var usersDtos = _mapper.Map<IEnumerable<UserDtoRead>>(users);

        return Ok(usersDtos);   
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserAsync([FromRoute]string id)
    {
        var user = await _userRepository.GetUserAsync(id);

        var userDto = _mapper.Map<UserDtoRead>(user);
        return Ok(userDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUserAsync([FromRoute] string id, [FromBody]UserDtoUpdate userUpdateDto)
    {
        var user = await _userRepository.GetUserAsync(id);

        userUpdateDto.ToUserModel(user);

        await _userRepository.UpdateUserAsync(user);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser([FromRoute]string id)
    {
        var user = await _userRepository.GetUserAsync(id);

        await _userRepository.DeleteUserAsync(user);

        return NoContent();
    }
}
