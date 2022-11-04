using RunAndHikeStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunAndHikeStore.Data.Seeding
{
    internal class ProductTypeSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.ProductTypes.Any())
            {
                return;
            }

            var initialProductTypes= new List<ProductType>()
                                            {
                                              new ProductType
                                              {
                                                  Id = "0a0e1278-6957-477f-bd05-4839e0f7de83",
                                                  Name = "Shoes",
                                              },
                                              new ProductType
                                              {
                                                  Id = "d2cefad3-9f34-4256-bfbd-23a875436450",
                                                  Name = "Accessories",
                                              },
                                              new ProductType
                                              {
                                                  Id = "4a014878-5493-4e57-b957-5362756fd7d6",
                                                  Name = "Clothing",
                                              },
                                            };

            await dbContext.ProductTypes.AddRangeAsync(initialProductTypes);
        }
    }
}
