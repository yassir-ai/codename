using UserService.Dtos;
using UserService.Model;

namespace UserService.Extensions;

public static class UserExtensions
{
    public static void ToUserModel(this UserDtoUpdate userUpdateDto, User user)
    {
        user.Name = userUpdateDto.Name;
    }
}
