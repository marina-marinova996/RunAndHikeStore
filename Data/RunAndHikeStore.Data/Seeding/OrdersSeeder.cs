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
                                                  Id = "618695c2-17cd-46ea-8ec7-d8c730295903",
                                                  OrderNumber = "45869781",
                                                  OrderDate = new DateTime(2022, 11, 22),
                                                  OrderStatus = Models.Enums.OrderStatus.Approved,
                                                  PaymentStatus = Models.Enums.PaymentStatus.NotPaid,
                                                  CustomerId = "6e736140-d201-4e92-afe8-d52895ec1bc2",
                                                  BillingDetailsId = "479939b8-ea8e-4daa-8c81-0635a3f7ff72",
                                              },
                                              new Order
                                              {
                                                  Id = "718695c2-17cd-46ea-8ec7-d8c730295904",
                                                  OrderNumber = "5869782",
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
