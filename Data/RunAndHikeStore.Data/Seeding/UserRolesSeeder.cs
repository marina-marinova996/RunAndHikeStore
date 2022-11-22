﻿using Microsoft.AspNetCore.Identity;
using RunAndHikeStore.Common;
using RunAndHikeStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunAndHikeStore.Data.Seeding
{
    internal class UserRolesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.UserRoles.Any())
            {
                return;
            }

            var userRole = new IdentityUserRole<string>()
            {
                UserId = "bc519db8-e466-49ed-a0b4-0ea89282c076",
                RoleId = "15852114-4f40-4748-95a0-77f1567d838f",
            };

            await dbContext.UserRoles.AddAsync(userRole);
        }
    }
}
