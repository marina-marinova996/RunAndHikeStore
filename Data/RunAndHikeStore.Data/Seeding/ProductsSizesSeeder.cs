using RunAndHikeStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunAndHikeStore.Data.Seeding
{
    internal class ProductsSizesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.ProductsSizes.Any())
            {
                return;
            }

            var initialProductsSizes = new List<ProductSize>()
                                             {
                                              new ProductSize
                                              {
                                                  ProductId = "3bbf392e-9cd6-47bf-8ed1-1666c2dc6065",
                                                  SizeId = "2960e803-34ef-4472-9fc4-3e791f739db6",
                                                  UnitsInStock = 20,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "3bbf392e-9cd6-47bf-8ed1-1666c2dc6065",
                                                  SizeId = "23ba220e-778d-4ef0-a381-b7bbcc7a68f2",
                                                  UnitsInStock = 20,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "3bbf392e-9cd6-47bf-8ed1-1666c2dc6065",
                                                  SizeId = "d0242cc4-ea2a-4098-896d-88f609fca7df",
                                                  UnitsInStock = 10,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "3bbf392e-9cd6-47bf-8ed1-1666c2dc6065",
                                                  SizeId = "e0670b96-58c4-43c5-a427-90fa5599213c",
                                                  UnitsInStock = 5,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "3dce6752-f87c-4eb7-b80d-4a769c614c07",
                                                  SizeId = "ec658186-250e-45fa-a627-4d4eeffa770a",
                                                  UnitsInStock = 8,

                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "3dce6752-f87c-4eb7-b80d-4a769c614c07",
                                                  SizeId = "2960e803-34ef-4472-9fc4-3e791f739db6",
                                                  UnitsInStock = 8,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "3dce6752-f87c-4eb7-b80d-4a769c614c07",
                                                  SizeId = "23ba220e-778d-4ef0-a381-b7bbcc7a68f2",
                                                  UnitsInStock = 8,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "3dce6752-f87c-4eb7-b80d-4a769c614c07",
                                                  SizeId = "d0242cc4-ea2a-4098-896d-88f609fca7df",
                                                  UnitsInStock = 8,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "3dce6752-f87c-4eb7-b80d-4a769c614c07",
                                                  SizeId = "e0670b96-58c4-43c5-a427-90fa5599213c",
                                                  UnitsInStock = 8,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "910f54e9-302d-4551-9590-c20e7e3164aa",
                                                  SizeId = "a044452a-73dd-47ef-af7c-c707049b6dc7",
                                                  UnitsInStock = 20,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "910f54e9-302d-4551-9590-c20e7e3164aa",
                                                  SizeId = "9a0fe242-c12b-426d-9432-72261d5743c4",
                                                  UnitsInStock = 20,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "910f54e9-302d-4551-9590-c20e7e3164aa",
                                                  SizeId = "c96da87a-751b-49cc-8d37-15c9aaaecfa9",
                                                  UnitsInStock = 20,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "910f54e9-302d-4551-9590-c20e7e3164aa",
                                                  SizeId = "2f5182e9-1732-4793-a42e-14dda9b1ddb4",
                                                  UnitsInStock = 20,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "910f54e9-302d-4551-9590-c20e7e3164aa",
                                                  SizeId = "ca565f74-ac40-41ef-ae7e-301112eac217",
                                                  UnitsInStock = 20,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "910f54e9-302d-4551-9590-c20e7e3164aa",
                                                  SizeId = "0510d990-8d50-4c57-a116-3b66cd04d8fe",
                                                  UnitsInStock = 20,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "910f54e9-302d-4551-9590-c20e7e3164aa",
                                                  SizeId = "b4bbc79c-6b94-4e26-9a22-8e5b743d1bd6",
                                                  UnitsInStock = 10,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "910f54e9-302d-4551-9590-c20e7e3164aa",
                                                  SizeId = "8d547016-c198-4c35-a4a4-9a0bf171011c",
                                                  UnitsInStock = 10,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "910f54e9-302d-4551-9590-c20e7e3164aa",
                                                  SizeId = "4fff4569-e06f-45d1-9dea-308f40854305",
                                                  UnitsInStock = 10,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "e112a421-e2b1-46b4-9d38-5f6c3773b027",
                                                  SizeId = "a044452a-73dd-47ef-af7c-c707049b6dc7",
                                                  UnitsInStock = 10,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "e112a421-e2b1-46b4-9d38-5f6c3773b027",
                                                  SizeId = "9a0fe242-c12b-426d-9432-72261d5743c4",
                                                  UnitsInStock = 10,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "e112a421-e2b1-46b4-9d38-5f6c3773b027",
                                                  SizeId = "c96da87a-751b-49cc-8d37-15c9aaaecfa9",
                                                  UnitsInStock = 10,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "e112a421-e2b1-46b4-9d38-5f6c3773b027",
                                                  SizeId = "2f5182e9-1732-4793-a42e-14dda9b1ddb4",
                                                  UnitsInStock = 10,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "e112a421-e2b1-46b4-9d38-5f6c3773b027",
                                                  SizeId = "ca565f74-ac40-41ef-ae7e-301112eac217",
                                                  UnitsInStock = 10,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "e112a421-e2b1-46b4-9d38-5f6c3773b027",
                                                  SizeId = "0510d990-8d50-4c57-a116-3b66cd04d8fe",
                                                  UnitsInStock = 10,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "e112a421-e2b1-46b4-9d38-5f6c3773b027",
                                                  SizeId = "b4bbc79c-6b94-4e26-9a22-8e5b743d1bd6",
                                                  UnitsInStock = 10,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "e112a421-e2b1-46b4-9d38-5f6c3773b027",
                                                  SizeId = "8d547016-c198-4c35-a4a4-9a0bf171011c",
                                                  UnitsInStock = 10,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "3c70d9d3-8a16-4f08-b7de-305007090be4",
                                                  SizeId = "4fff4569-e06f-45d1-9dea-308f40854305",
                                                  UnitsInStock = 10,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "b6316380-6e25-404e-b8e9-4571add0aea1",
                                                  SizeId = "a044452a-73dd-47ef-af7c-c707049b6dc7",
                                                  UnitsInStock = 10,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "b6316380-6e25-404e-b8e9-4571add0aea1",
                                                  SizeId = "9a0fe242-c12b-426d-9432-72261d5743c4",
                                                  UnitsInStock = 15,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "b6316380-6e25-404e-b8e9-4571add0aea1",
                                                  SizeId = "c96da87a-751b-49cc-8d37-15c9aaaecfa9",
                                                  UnitsInStock = 15,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "b6316380-6e25-404e-b8e9-4571add0aea1",
                                                  SizeId = "2f5182e9-1732-4793-a42e-14dda9b1ddb4",
                                                  UnitsInStock = 15,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "b6316380-6e25-404e-b8e9-4571add0aea1",
                                                  SizeId = "ca565f74-ac40-41ef-ae7e-301112eac217",
                                                  UnitsInStock = 15,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "b6316380-6e25-404e-b8e9-4571add0aea1",
                                                  SizeId = "0510d990-8d50-4c57-a116-3b66cd04d8fe",
                                                  UnitsInStock = 15,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "b6316380-6e25-404e-b8e9-4571add0aea1",
                                                  SizeId = "8d547016-c198-4c35-a4a4-9a0bf171011c",
                                                  UnitsInStock = 15,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "b6316380-6e25-404e-b8e9-4571add0aea1",
                                                  SizeId = "4fff4569-e06f-45d1-9dea-308f40854305",
                                                  UnitsInStock = 15,
                                              },
                                             };

            await dbContext.ProductsSizes.AddRangeAsync(initialProductsSizes);
        }
    }
}
