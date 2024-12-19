using System.ComponentModel.DataAnnotations;

namespace UserService.Model;

public class User
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public bool InSession { get; set; } = false;
    public int? SessionId { get; set; }
}