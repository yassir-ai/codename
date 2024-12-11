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
    public async Task<IActionResult> CreateUser([FromBody] UserDtoCreate userCreateDto)
    {
        var user = _mapper.Map<User>(userCreateDto);

        _userRepository.CreateUser(user);

        return Created();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = _userRepository.GetAllUsers();

        var usersDtos = _mapper.Map<IEnumerable<UserDtoRead>>(users);

        return Ok(usersDtos);   
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser([FromRoute]string id)
    {
        var user = _userRepository.GetUser(id);

        var userDto = _mapper.Map<UserDtoRead>(user);
        return Ok(userDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser([FromRoute] string id, [FromBody]UserDtoUpdate userUpdateDto)
    {
        var user = _userRepository.GetUser(id);

        userUpdateDto.ToUserModel(user);

        _userRepository.UpdateUser(user);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser([FromRoute]string id)
    {
        var user = _userRepository.GetUser(id);

        _userRepository.DeleteUser(user);

        return NoContent();
    }
}
