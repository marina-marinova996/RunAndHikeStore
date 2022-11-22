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

            roles.Add(adminRole);

            await dbContext.Roles.AddRangeAsync(roles);
        }
    }
}
