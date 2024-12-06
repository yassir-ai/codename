using AutoMapper;
using UserService.Dto;
using UserService.Dtos;
using UserService.Model;

namespace UserService.Mappers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        //source --> dest
        CreateMap<UserDtoCreate, User>();
        CreateMap<User, UserDtoRead>();
    }
}
