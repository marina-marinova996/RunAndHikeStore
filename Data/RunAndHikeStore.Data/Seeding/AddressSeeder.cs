using System;
using System.Threading.Tasks;

namespace RunAndHikeStore.Data.Seeding
{
    internal class AddressSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            //if (dbContext.Addresses.Any())
            //{
            //    return;
            //}

            //var initialAddresses = new List<Address>()
            //                                {
            //                                  new Address {},
            //                                  new Address {},
            //                                  new Address {},
            //                                  new Address {},
            //                                  new Address {},
            //                                  new Address {},
            //                                };

            //await dbContext.Addresses.AddRangeAsync(initialAddresses);
        }
    }
}
