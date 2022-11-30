using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using RunAndHikeStore.Data.Models;
using System;
using System.Linq;
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

            var firstUser = new ApplicationUser()
            {
                Id = "bc519db8-e466-49ed-a0b4-0ea89282c076",
                FirstName = "Admin",
                LastName = "User",
                Email = "admin@runandhikestore.com",
                UserName = "admin@runandhikestore.com",
            };

            var secondUser = new ApplicationUser()
            {
                Id = "6e736140-d201-4e92-afe8-d52895ec1bc2",
                FirstName = "Ivan",
                LastName = "Petrov",
                Email = "ivan@gmail.com",
                UserName = "ivan@gmail.com",
            };

            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var resultFirstUser = await userManager.CreateAsync(firstUser, "123456Ab!");
            var resultSecondUser = await userManager.CreateAsync(secondUser, "Ab123456!");
        }
    }
}
