using Microsoft.EntityFrameworkCore;
using UserService.Model;

namespace UserService.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}

        public DbSet<User> Users { get; set; }
    }
}