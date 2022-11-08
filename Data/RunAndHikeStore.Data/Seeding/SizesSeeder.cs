namespace RunAndHikeStore.Data.Seeding
{
    using RunAndHikeStore.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    internal class SizesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Sizes.Any())
            {
                return;
            }

            List<Size> initialSizesShoes = new List<Size>
                                                           {
                                                            new Size
                                                            {
                                                               Id = "ec658186-250e-45fa-a627-4d4eeffa770a",
                                                               Name = "36",
                                                               ProductTypeId = "0a0e1278-6957-477f-bd05-4839e0f7de83",
                                                            },
                                                            new Size
                                                            {
                                                                Id = "2960e803-34ef-4472-9fc4-3e791f739db6",
                                                                Name = "37",
                                                                ProductTypeId = "0a0e1278-6957-477f-bd05-4839e0f7de83",
                                                            },
                                                            new Size
                                                            {
                                                                Id = "23ba220e-778d-4ef0-a381-b7bbcc7a68f2",
                                                                Name = "38",
                                                                ProductTypeId = "0a0e1278-6957-477f-bd05-4839e0f7de83",
                                                            },
                                                            new Size
                                                            {
                                                                Id = "d0242cc4-ea2a-4098-896d-88f609fca7df",
                                                                Name = "39",
                                                                ProductTypeId = "0a0e1278-6957-477f-bd05-4839e0f7de83",
                                                            },
                                                            new Size
                                                            {
                                                                Id = "e0670b96-58c4-43c5-a427-90fa5599213c",
                                                                Name = "40",
                                                                ProductTypeId = "0a0e1278-6957-477f-bd05-4839e0f7de83",
                                                            },
                                                            new Size
                                                            {
                                                                Id = "2f5182e9-1732-4793-a42e-14dda9b1ddb4",
                                                                Name = "41",
                                                                ProductTypeId = "0a0e1278-6957-477f-bd05-4839e0f7de83",
                                                            },
                                                            new Size
                                                            {
                                                                Id = "ca565f74-ac40-41ef-ae7e-301112eac217",
                                                                Name = "42",
                                                                ProductTypeId = "0a0e1278-6957-477f-bd05-4839e0f7de83",
                                                            },
                                                            new Size
                                                            {
                                                                Id = "0510d990-8d50-4c57-a116-3b66cd04d8fe",
                                                                Name = "43",
                                                                ProductTypeId = "0a0e1278-6957-477f-bd05-4839e0f7de83",
                                                            },
                                                            new Size
                                                            {
                                                                Id = "b4bbc79c-6b94-4e26-9a22-8e5b743d1bd6",
                                                                Name = "44",
                                                                ProductTypeId = "0a0e1278-6957-477f-bd05-4839e0f7de83",
                                                            },
                                                            new Size
                                                            {
                                                                Id = "8d547016-c198-4c35-a4a4-9a0bf171011c",
                                                                Name = "45",
                                                                ProductTypeId = "0a0e1278-6957-477f-bd05-4839e0f7de83",
                                                            },
                                                            new Size
                                                            {
                                                                Id = "4fff4569-e06f-45d1-9dea-308f40854305",
                                                                Name = "46",
                                                                ProductTypeId = "0a0e1278-6957-477f-bd05-4839e0f7de83",
                                                            },
                                                           };

            List<Size> initialSizesClothesAndAccessories = new List<Size>
            {
                                                            new Size
                                                            {
                                                                Id = "15012192-b182-4a3c-affc-571c85946a52",
                                                                Name = "XS",
                                                                ProductTypeId = "4a014878-5493-4e57-b957-5362756fd7d6",
                                                            },
                                                            new Size
                                                            {
                                                                Id = "fd590f2e-7901-497d-bd09-b3c721da851c",
                                                                Name = "S",
                                                                ProductTypeId = "4a014878-5493-4e57-b957-5362756fd7d6",
                                                            },
                                                            new Size
                                                            {
                                                                Id = "a41aa263-cc2f-42bf-ae0d-da22ddb27e0c",
                                                                Name = "M",
                                                                ProductTypeId = "4a014878-5493-4e57-b957-5362756fd7d6",
                                                            },
                                                            new Size
                                                            {
                                                                Id = "25484640-9445-4f3f-87b6-4b4f0c1cdff8",
                                                                Name = "L",
                                                                ProductTypeId = "4a014878-5493-4e57-b957-5362756fd7d6",
                                                            },
                                                            new Size
                                                            {
                                                                Id = "46ed5876-2a3f-4f5b-95d6-50196a4cbe12",
                                                                Name = "XL",
                                                                ProductTypeId = "4a014878-5493-4e57-b957-5362756fd7d6",
                                                            },
                                                            new Size
                                                            {
                                                                Id = "273fd7ba-8e4f-4850-9b98-c5ed58d9129e",
                                                                Name = "2XL",
                                                                ProductTypeId = "4a014878-5493-4e57-b957-5362756fd7d6",
                                                            },
                                                            new Size
                                                            {
                                                                Id = "69a71fc9-a742-4f5d-8626-d57dc9c47c5c",
                                                                Name = "One Size",
                                                                ProductTypeId = "d2cefad3-9f34-4256-bfbd-23a875436450",
                                                            },
            };

            await dbContext.Sizes.AddRangeAsync(initialSizesShoes);
            await dbContext.Sizes.AddRangeAsync(initialSizesClothesAndAccessories);
        }
    }
}