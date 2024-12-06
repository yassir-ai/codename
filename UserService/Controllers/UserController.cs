using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UserService.Dtos;
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
    public IActionResult CreateUser([FromBody] UserDtoCreate userCreateDto)
    {
        if(!ModelState.IsValid) return BadRequest();

        var user = _mapper.Map<User>(userCreateDto);

        if(! _userRepository.CreateUser(user)) return StatusCode(500, "An unexpected error occurred");

        return Created();
    }

    [HttpGet]
    public IActionResult GetAllUsers()
    {
        try
        {
            var users = _userRepository.GetAllUsers();

            if (!users.Any()) return NotFound();

            var usersDtos = _mapper.Map<IEnumerable<UserDtoRead>>(users);

            return Ok(usersDtos);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
        }
        
    }

    [HttpGet("{id}")]
    public IActionResult GetUser([FromRoute]string id)
    {
        try
        {
            var user = _userRepository.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            var userDto = _mapper.Map<UserDtoRead>(user);
            return Ok(userDto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
        }
    }
}
