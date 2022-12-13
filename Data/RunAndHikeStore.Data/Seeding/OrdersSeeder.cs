using Microsoft.EntityFrameworkCore;
using RunAndHikeStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunAndHikeStore.Data.Seeding
{
    internal class OrdersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Orders.Any())
            {
                return;
            }

            var initialOrders = new List<Order>()
                                            {
                                              new Order
                                              {
                                                  Id = "1",
                                                  OrderDate = new DateTime(2022, 11, 22),
                                                  OrderStatus = Models.Enums.OrderStatus.Approved,
                                                  PaymentStatus = Models.Enums.PaymentStatus.NotPaid,
                                                  CustomerId = "6e736140-d201-4e92-afe8-d52895ec1bc2",
                                                  BillingDetailsId = "479939b8-ea8e-4daa-8c81-0635a3f7ff72",
                                              },
                                              new Order
                                              {
                                                  Id = "2",
                                                  OrderDate = new DateTime(2022, 11, 18),
                                                  OrderStatus = Models.Enums.OrderStatus.Approved,
                                                  PaymentStatus = Models.Enums.PaymentStatus.Paid,
                                                  CustomerId = "6e736140-d201-4e92-afe8-d52895ec1bc2",
                                                  BillingDetailsId = "479939b8-ea8e-4daa-8c81-0635a3f7ff72",
                                              },
                                            };

            await dbContext.Orders.AddRangeAsync(initialOrders);
        }
    }
}
