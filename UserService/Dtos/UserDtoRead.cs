namespace UserService.Dtos;

public class UserDtoRead
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public bool InSession { get; set; } = false;
    public int? SessionId { get; set; }
}
