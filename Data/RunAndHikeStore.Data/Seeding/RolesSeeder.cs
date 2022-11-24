namespace RunAndHikeStore.Data.Seeding
{
    using RunAndHikeStore.Common;
    using RunAndHikeStore.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    internal class RolesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Roles.Any())
            {
                return;
            }

            var roles = new List<ApplicationRole>();

            var adminRole = new ApplicationRole()
            {
                Id = "15852114-4f40-4748-95a0-77f1567d838f",
                Name = GlobalConstants.AdministratorRoleName,
                NormalizedName = GlobalConstants.NormalizedAdministratorRoleName,
            };

            var userRole = new ApplicationRole()
            {
                Id = "26852114-4f40-4748-95a0-77f1567d837a",
                Name = GlobalConstants.UserRoleName,
                NormalizedName = GlobalConstants.NormalizedUserRoleName,
            };

            roles.Add(adminRole);
            roles.Add(userRole);

            await dbContext.Roles.AddRangeAsync(roles);
        }
    }
}
