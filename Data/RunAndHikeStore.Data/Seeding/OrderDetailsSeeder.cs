namespace RunAndHikeStore.Data.Seeding
{
    using RunAndHikeStore.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    internal class OrderDetailsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.OrderDetails.Any())
            {
                return;
            }

            var initialOrderDetails = new List<OrderDetail>()
                                            {
                                              new OrderDetail
                                              {
                                                  OrderId =  "1",
                                                  ProductId = "8c30054f-205c-4531-8249-d1490594daab",
                                                  OrderQuantity = 1,
                                                  UnitPrice = 220m,
                                                  Size = "S",
                                              },
                                              new OrderDetail
                                              {
                                                  OrderId = "1",
                                                  ProductId = "99214889-61aa-4285-a80b-1ae5281f11b0",
                                                  OrderQuantity = 1,
                                                  UnitPrice = 249.99m,
                                                  Size = "One Size",
                                              },
                                              new OrderDetail
                                              {
                                                  OrderId = "2",
                                                  ProductId = "f6e4d33b-8704-44f8-b83f-6992563c222c",
                                                  OrderQuantity = 2,
                                                  UnitPrice = 35m,
                                                  Size = "L",
                                              },
                                            };

            await dbContext.OrderDetails.AddRangeAsync(initialOrderDetails);
        }
    }
}
