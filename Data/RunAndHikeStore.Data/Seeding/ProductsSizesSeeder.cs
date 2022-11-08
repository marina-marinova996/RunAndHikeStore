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
                                                  ProductId = "99214889-61aa-4285-a80b-1ae5281f11bb",
                                                  SizeId = "69a71fc9-a742-4f5d-8626-d57dc9c47c5c",
                                                  UnitsInStock = 20,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "99214889-61aa-4285-a80b-1ae5281f11ba",
                                                  SizeId = "69a71fc9-a742-4f5d-8626-d57dc9c47c5c",
                                                  UnitsInStock = 20,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "99214889-61aa-4285-a80b-1ae5281f11b0",
                                                  SizeId = "69a71fc9-a742-4f5d-8626-d57dc9c47c5c",
                                                  UnitsInStock = 15,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "3bbf392e-9cd6-47bf-8ed1-1666c2dc6065",
                                                  SizeId = "2960e803-34ef-4472-9fc4-3e791f739db6",
                                                  UnitsInStock = 10,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "3bbf392e-9cd6-47bf-8ed1-1666c2dc6065",
                                                  SizeId = "23ba220e-778d-4ef0-a381-b7bbcc7a68f2",
                                                  UnitsInStock = 5,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "3bbf392e-9cd6-47bf-8ed1-1666c2dc6065",
                                                  SizeId = "e0670b96-58c4-43c5-a427-90fa5599213c",
                                                  UnitsInStock = 8,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "2ae0c740-a032-4413-bec8-77df54439edd",
                                                  SizeId = "d0242cc4-ea2a-4098-896d-88f609fca7df",
                                                  UnitsInStock = 8,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "3c70d9d3-8a16-4f08-b7de-305007090be4",
                                                  SizeId = "ca565f74-ac40-41ef-ae7e-301112eac217",
                                                  UnitsInStock = 8,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "3c70d9d3-8a16-4f08-b7de-305007090be4",
                                                  SizeId = "b4bbc79c-6b94-4e26-9a22-8e5b743d1bd6",
                                                  UnitsInStock = 8,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "3c70d9d3-8a16-4f08-b7de-305007090be4",
                                                  SizeId = "8d547016-c198-4c35-a4a4-9a0bf171011c",
                                                  UnitsInStock = 8,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "e112a421-e2b1-46b4-9d38-5f6c3773b027",
                                                  SizeId = "2f5182e9-1732-4793-a42e-14dda9b1ddb4",
                                                  UnitsInStock = 20,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "e112a421-e2b1-46b4-9d38-5f6c3773b027",
                                                  SizeId = "b4bbc79c-6b94-4e26-9a22-8e5b743d1bd6",
                                                  UnitsInStock = 20,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "e112a421-e2b1-46b4-9d38-5f6c3773b027",
                                                  SizeId = "8d547016-c198-4c35-a4a4-9a0bf171011c",
                                                  UnitsInStock = 20,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "910f54e9-302d-4551-9590-c20e7e3164aa",
                                                  SizeId = "2960e803-34ef-4472-9fc4-3e791f739db6",
                                                  UnitsInStock = 20,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "910f54e9-302d-4551-9590-c20e7e3164aa",
                                                  SizeId = "23ba220e-778d-4ef0-a381-b7bbcc7a68f2",
                                                  UnitsInStock = 10,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "910f54e9-302d-4551-9590-c20e7e3164aa",
                                                  SizeId = "d0242cc4-ea2a-4098-896d-88f609fca7df",
                                                  UnitsInStock = 10,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "b6316380-6e25-404e-b8e9-4571add0aea1",
                                                  SizeId = "0510d990-8d50-4c57-a116-3b66cd04d8fe",
                                                  UnitsInStock = 10,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "b6316380-6e25-404e-b8e9-4571add0aea1",
                                                  SizeId = "b4bbc79c-6b94-4e26-9a22-8e5b743d1bd6",
                                                  UnitsInStock = 10,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "8c30054f-205c-4531-8249-d1490594daab",
                                                  SizeId = "fd590f2e-7901-497d-bd09-b3c721da851c",
                                                  UnitsInStock = 10,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "8c30054f-205c-4531-8249-d1490594daab",
                                                  SizeId = "a41aa263-cc2f-42bf-ae0d-da22ddb27e0c",
                                                  UnitsInStock = 10,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "8c30054f-205c-4531-8249-d1490594daab",
                                                  SizeId = "25484640-9445-4f3f-87b6-4b4f0c1cdff8",
                                                  UnitsInStock = 10,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "3f671721-f5e4-4c98-84ef-199f389707fc",
                                                  SizeId = "25484640-9445-4f3f-87b6-4b4f0c1cdff8",
                                                  UnitsInStock = 10,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "3dce6752-f87c-4eb7-b80d-4a769c614c07",
                                                  SizeId = "15012192-b182-4a3c-affc-571c85946a52",
                                                  UnitsInStock = 10,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "3dce6752-f87c-4eb7-b80d-4a769c614c07",
                                                  SizeId = "fd590f2e-7901-497d-bd09-b3c721da851c",
                                                  UnitsInStock = 10,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "3dce6752-f87c-4eb7-b80d-4a769c614c07",
                                                  SizeId = "a41aa263-cc2f-42bf-ae0d-da22ddb27e0c",
                                                  UnitsInStock = 10,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "f6e4d33b-8704-44f8-b83f-6992563c222c",
                                                  SizeId = "25484640-9445-4f3f-87b6-4b4f0c1cdff8",
                                                  UnitsInStock = 10,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "f6e4d33b-8704-44f8-b83f-6992563c222c",
                                                  SizeId = "46ed5876-2a3f-4f5b-95d6-50196a4cbe12",
                                                  UnitsInStock = 10,
                                              },
                                              new ProductSize
                                              {
                                                  ProductId = "f6e4d33b-8704-44f8-b83f-6992563c222c",
                                                  SizeId = "273fd7ba-8e4f-4850-9b98-c5ed58d9129e",
                                                  UnitsInStock = 15,
                                              },
                                             };

            await dbContext.ProductsSizes.AddRangeAsync(initialProductsSizes);
        }
    }
}
