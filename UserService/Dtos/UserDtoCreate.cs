using System.ComponentModel.DataAnnotations;

namespace UserService.Dtos;

public class UserDtoCreate
{
    [Required]
    [MaxLength(30)]
    [MinLength(3)]
    public string Name { get; set; }
}
