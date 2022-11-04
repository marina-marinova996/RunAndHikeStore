namespace RunAndHikeStore.Data.Seeding
{
    using RunAndHikeStore.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    internal class BrandsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Brands.Any())
            {
                return;
            }

            var initialBrands = new List<Brand>()
                                            {
                                              new Brand
                                              {
                                                  Id = "f1d8af84-411d-4a94-b5b0-4ee577b98c19",
                                                  Name = "Salomon",
                                              },
                                              new Brand
                                              {
                                                  Id = "95a5446c-d8c6-48ba-a58b-e1d81b958eec",
                                                  Name = "Hoka One One",
                                              },
                                              new Brand
                                              {
                                                  Id = "04293eea-02c7-4bc4-a13c-7edd2edb6e28",
                                                  Name = "La Sportiva",
                                              },
                                              new Brand
                                              {
                                                  Id = "f1b41645-1b83-4975-a5d1-26c713c25321",
                                                  Name = "Coros",
                                              },
                                              new Brand
                                              {
                                                  Id = "761fe0f9-dda4-48a8-bcaa-0dd04e0bc2d7",
                                                  Name = "Garmin",
                                              },
                                              new Brand
                                              {
                                                  Id = "a1b82e61-1900-427e-927c-492dc771e9c0",
                                                  Name = "Saucony",
                                              },
                                              new Brand
                                              {
                                                  Id = "f3321950-dfd8-4635-83fa-c3a5b9888daf",
                                                  Name = "Jack Wolfskin",
                                              },
                                              new Brand
                                              {
                                                  Id = "f94d2830-7038-4c14-a724-701db2179091",
                                                  Name = "North Face",
                                              },
                                            };

            await dbContext.Brands.AddRangeAsync(initialBrands);
        }
    }
}
