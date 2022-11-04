namespace RunAndHikeStore.Data.Seeding
{
    using System;
    using System.Threading.Tasks;
    internal class OrderDetailsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            //if (dbContext.OrderDetails.Any())
            //{
            //    return;
            //}

            //var initialOrderDetails = new List<OrderDetail>()
            //                                {
            //                                  new OrderDetail {},
            //                                  new OrderDetail {},
            //                                  new OrderDetail {},
            //                                };

            //await dbContext.OrderDetails.AddRangeAsync(initialOrderDetails);
        }
    }
}
