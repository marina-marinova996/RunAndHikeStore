using RunAndHikeStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunAndHikeStore.Data.Seeding
{
    internal class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            var initialCategories = new List<Category>()
                                            {
                                              new Category
                                              {
                                                  Id = "f7cdc372-f45c-486a-83c7-75ba640352e8",
                                                  Name = "Trail running",
                                              },
                                              new Category
                                              {
                                                  Id = "8d36fd4f-0bbc-4fc4-a277-36f1d9657b8f",
                                                  Name = "Running",
                                              },
                                              new Category
                                              {
                                                  Id = "c3e985f9-3450-441b-b12a-f922bb5d67b7",
                                                  Name = "Hiking",
                                              },
                                            };

            await dbContext.Categories.AddRangeAsync(initialCategories);
        }
    }
}
