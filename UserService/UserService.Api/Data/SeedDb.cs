using UserService.Model;

namespace UserService.Data
{
    static class SeedDb
    {
        static public void Apply(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<DataContext>();

                Seed(dbContext);
            }
        }

        static private void Seed(DataContext dbContext)
        {
            var users = new List<User>
            {
                new User
                {
                    Name = "Alice",
                    CreatedAt = DateTime.Now,
                    InSession = true,
                    SessionId = 1
                },
                new User
                {
                    Name = "Bob",
                    CreatedAt = DateTime.Now.AddDays(-1),
                    InSession = false,
                    SessionId = null
                },
                new User
                {
                    Name = "Charlie",
                    CreatedAt = DateTime.Now.AddHours(-2),
                    InSession = false,
                    SessionId = null
                },
                new User
                {
                    Name = "Xia",
                    CreatedAt = DateTime.Now.AddHours(-3),
                    InSession = false,
                    SessionId = null
                },
                new User
                {
                    Name = "Solene",
                    CreatedAt = DateTime.Now.AddHours(5),
                    InSession = false,
                    SessionId = null
                },
                new User
                {
                    Name = "Laura",
                    CreatedAt = DateTime.Now.AddHours(1),
                    InSession = false,
                    SessionId = null
                }
            };

            dbContext.Users.AddRange(users);
            dbContext.SaveChanges();
        }
    }
}