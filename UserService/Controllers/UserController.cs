using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UserService.Dto;
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
}
