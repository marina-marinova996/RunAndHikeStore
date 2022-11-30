namespace RunAndHikeStore.Data.Seeding
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ApplicationDbContextSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            var logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger(typeof(ApplicationDbContextSeeder));

            var seeders = new List<ISeeder>
                          {
                              new SettingsSeeder(),
                              new BrandsSeeder(),
                              new ProductTypeSeeder(),
                              new ProductsSeeder(),
                              new CategoriesSeeder(),
                              new CategoriesProductsSeeder(),
                              new SizesSeeder(),
                              new ProductsSizesSeeder(),
                              new UsersSeeder(),
                              new RolesSeeder(),
                              new UserRolesSeeder(),
                              new BillingDetailsSeeder(),
                              new OrdersSeeder(),
                              new OrderDetailsSeeder(),
                              new AddressSeeder(),
                          };

            foreach (var seeder in seeders)
            {
                await seeder.SeedAsync(dbContext, serviceProvider);
                await dbContext.SaveChangesAsync();
                logger.LogInformation($"Seeder {seeder.GetType().Name} done.");
            }
        }
    }
}
