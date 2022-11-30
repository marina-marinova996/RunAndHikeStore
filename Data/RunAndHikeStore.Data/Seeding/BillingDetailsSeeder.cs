using RunAndHikeStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunAndHikeStore.Data.Seeding
{
    internal class BillingDetailsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.BillingDetails.Any())
            {
                return;
            }

            var initialBillingDetails = new List<BillingDetails>()
                                            {
                                              new BillingDetails
                                              {
                                                  Id = "479939b8-ea8e-4daa-8c81-0635a3f7ff72",
                                                  FirstName = "Ivan",
                                                  LastName = "Petrov",
                                                  StreetAddress = "Osmi Primorski Polk 2",
                                                  City = "Varna",
                                                  Country = "Bulgaria",
                                                  PostalCode = "9000",
                                                  PhoneNumber = "+359899665544",
                                                  CustomerId = "6e736140-d201-4e92-afe8-d52895ec1bc2"
                                              },
                                            };

            await dbContext.BillingDetails.AddRangeAsync(initialBillingDetails);
        }
    }
}
