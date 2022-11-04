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

            List<Size> initialSizesShoesWomen = new List<Size>
                                                           {
                                                            new Size
                                                            {
                                                               Id = "ec658186-250e-45fa-a627-4d4eeffa770a",
                                                               Name = "36",
                                                               Gender = Models.Enums.Gender.Female,
                                                               IsInteger = true,
                                                            },
                                                            new Size
                                                            {
                                                                Id = "2960e803-34ef-4472-9fc4-3e791f739db6",
                                                                Name = "37", Gender = Models.Enums.Gender.Female,
                                                                IsInteger = true,
                                                            },
                                                            new Size
                                                            {
                                                                Id = "23ba220e-778d-4ef0-a381-b7bbcc7a68f2",
                                                                Name = "38",
                                                                Gender = Models.Enums.Gender.Female,
                                                                IsInteger = true,
                                                            },
                                                            new Size
                                                            {
                                                                Id = "d0242cc4-ea2a-4098-896d-88f609fca7df",
                                                                Name = "39",
                                                                Gender = Models.Enums.Gender.Female,
                                                                IsInteger = true,
                                                            },
                                                            new Size
                                                            {
                                                                Id = "e0670b96-58c4-43c5-a427-90fa5599213c",
                                                                Name = "40",
                                                                Gender = Models.Enums.Gender.Female,
                                                                IsInteger = true,
                                                            },
                                                           };

            List<Size> initialSizesShoesMen = new List<Size>
                                                           {
                                                            new Size
                                                            {
                                                                Id = "a044452a-73dd-47ef-af7c-c707049b6dc7",
                                                                Name = "38", Gender = Models.Enums.Gender.Male,
                                                                IsInteger = true,
                                                            },
                                                            new Size
                                                            {
                                                                Id = "9a0fe242-c12b-426d-9432-72261d5743c4",
                                                                Name = "39",
                                                                Gender = Models.Enums.Gender.Male,
                                                                IsInteger = true,
                                                            },
                                                            new Size
                                                            {
                                                                Id = "c96da87a-751b-49cc-8d37-15c9aaaecfa9",
                                                                Name = "40",
                                                                Gender = Models.Enums.Gender.Male,
                                                                IsInteger = true,
                                                            },
                                                            new Size
                                                            {
                                                                Id = "2f5182e9-1732-4793-a42e-14dda9b1ddb4",
                                                                Name = "41",
                                                                Gender = Models.Enums.Gender.Male,
                                                                IsInteger = true,
                                                            },
                                                            new Size
                                                            {
                                                                Id = "ca565f74-ac40-41ef-ae7e-301112eac217",
                                                                Name = "42",
                                                                Gender = Models.Enums.Gender.Male,
                                                                IsInteger = true,
                                                            },
                                                            new Size
                                                            {
                                                                Id = "0510d990-8d50-4c57-a116-3b66cd04d8fe",
                                                                Name = "43",
                                                                Gender = Models.Enums.Gender.Male,
                                                                IsInteger = true,
                                                            },
                                                            new Size
                                                            {
                                                                Id = "b4bbc79c-6b94-4e26-9a22-8e5b743d1bd6",
                                                                Name = "44",
                                                                Gender = Models.Enums.Gender.Male,
                                                                IsInteger = true,
                                                            },
                                                            new Size
                                                            {
                                                                Id = "8d547016-c198-4c35-a4a4-9a0bf171011c",
                                                                Name = "45",
                                                                Gender = Models.Enums.Gender.Male,
                                                                IsInteger = true,
                                                            },
                                                            new Size
                                                            {
                                                                Id = "4fff4569-e06f-45d1-9dea-308f40854305",
                                                                Name = "46",
                                                                Gender = Models.Enums.Gender.Male,
                                                                IsInteger = true,
                                                            },
                                                           };


            await dbContext.Sizes.AddRangeAsync(initialSizesShoesWomen);
            await dbContext.Sizes.AddRangeAsync(initialSizesShoesMen);
        }
    }
}