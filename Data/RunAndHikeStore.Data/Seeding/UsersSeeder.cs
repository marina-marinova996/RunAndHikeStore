using RunAndHikeStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunAndHikeStore.Data.Seeding
{
    internal class UsersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Users.Any())
            {
                return;
            }

            var initialUsers = new List<ApplicationUser>()
                                            {
                                              new ApplicationUser
                                              {
                                                  Id = "6a0e1278-6957-477f-bd05-4839e0f7de83",
                                                  FirstName = "Marina",
                                                  LastName = "Marinova",
                                                  Email = "marina@gmail.com",
                                              },
                                              new ApplicationUser
                                              {
                                                  Id = "52cefad3-9f34-4256-bfbd-23a875436450",
                                                  FirstName = "Ivan",
                                                  LastName = "Petrov",
                                                  Email = "ivan@gmail.com",
                                              },
                                            };

            await dbContext.Users.AddRangeAsync(initialUsers);
        }
    }
}
