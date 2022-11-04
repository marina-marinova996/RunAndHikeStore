using RunAndHikeStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunAndHikeStore.Data.Seeding
{
    internal class OrdersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            //if (dbContext.Orders.Any())
            //{
            //    return;
            //}

            //var initialOrders = new List<Order>()
            //                                {
            //                                  new Order {},
            //                                  new Order {},
            //                                  new Order {},
            //                                };

            //await dbContext.Orders.AddRangeAsync(initialOrders);
        }
    }
}
