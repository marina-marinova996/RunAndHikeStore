using RunAndHikeStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunAndHikeStore.Data.Seeding
{
    internal class AddressSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.DeliveryAddresses.Any())
            {
                return;
            }

            var initialAddresses = new List<Address>()
                                            {
                                              new Address
                                              {
                                                  Id = "579939b8-ea8e-4daa-8c81-0635a3f7ff73",
                                                  StreetAddress = "Osmi Primorski Polk 2",
                                                  City = "Varna",
                                                  Country = "Bulgaria",
                                                  PostalCode = "9000",
                                                  CustomerId = "6e736140-d201-4e92-afe8-d52895ec1bc2",
                                              },
                                            };

            await dbContext.DeliveryAddresses.AddRangeAsync(initialAddresses);
        }
    }
}
