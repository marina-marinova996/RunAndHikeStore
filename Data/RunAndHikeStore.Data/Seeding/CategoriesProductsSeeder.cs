using RunAndHikeStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunAndHikeStore.Data.Seeding
{
    internal class CategoriesProductsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.CategoriesProducts.Any())
            {
                return;
            }

            var initialProductsWithCategories = new List<CategoryProduct>()
                                             {
                                              new CategoryProduct
                                              {
                                                  CategoryId = "f7cdc372-f45c-486a-83c7-75ba640352e8",
                                                  ProductId = "3bbf392e-9cd6-47bf-8ed1-1666c2dc6065",
                                              },
                                              new CategoryProduct
                                              {
                                                  CategoryId = "f7cdc372-f45c-486a-83c7-75ba640352e8",
                                                  ProductId = "3dce6752-f87c-4eb7-b80d-4a769c614c07",
                                              },
                                              new CategoryProduct
                                              {
                                                  CategoryId = "f7cdc372-f45c-486a-83c7-75ba640352e8",
                                                  ProductId = "910f54e9-302d-4551-9590-c20e7e3164aa",
                                              },
                                              new CategoryProduct
                                              {
                                                  CategoryId = "f7cdc372-f45c-486a-83c7-75ba640352e8",
                                                  ProductId = "e112a421-e2b1-46b4-9d38-5f6c3773b027",
                                              },
                                              new CategoryProduct
                                              {
                                                  CategoryId = "f7cdc372-f45c-486a-83c7-75ba640352e8",
                                                  ProductId = "3c70d9d3-8a16-4f08-b7de-305007090be4",
                                              },
                                              new CategoryProduct
                                              {
                                                  CategoryId = "f7cdc372-f45c-486a-83c7-75ba640352e8",
                                                  ProductId = "b6316380-6e25-404e-b8e9-4571add0aea1",
                                              },
                                              new CategoryProduct
                                              {
                                                  CategoryId = "f7cdc372-f45c-486a-83c7-75ba640352e8",
                                                  ProductId = "2ae0c740-a032-4413-bec8-77df54439edd",
                                              },
                                              new CategoryProduct
                                              {
                                                  CategoryId = "c3e985f9-3450-441b-b12a-f922bb5d67b7",
                                                  ProductId = "8c30054f-205c-4531-8249-d1490594daab",
                                              },
                                              new CategoryProduct
                                              {
                                                  CategoryId = "c3e985f9-3450-441b-b12a-f922bb5d67b7",
                                                  ProductId = "3dce6752-f87c-4eb7-b80d-4a769c614c07",
                                              },
                                              new CategoryProduct
                                              {
                                                  CategoryId = "c3e985f9-3450-441b-b12a-f922bb5d67b7",
                                                  ProductId = "3f671721-f5e4-4c98-84ef-199f389707fc",
                                              },
                                              new CategoryProduct
                                              {
                                                  CategoryId = "c3e985f9-3450-441b-b12a-f922bb5d67b7",
                                                  ProductId = "f6e4d33b-8704-44f8-b83f-6992563c222c",
                                              },
                                              new CategoryProduct
                                              {
                                                  CategoryId = "f7cdc372-f45c-486a-83c7-75ba640352e8",
                                                  ProductId = "99214889-61aa-4285-a80b-1ae5281f11ba",
                                              },
                                              new CategoryProduct
                                              {
                                                  CategoryId = "8d36fd4f-0bbc-4fc4-a277-36f1d9657b8f",
                                                  ProductId = "99214889-61aa-4285-a80b-1ae5281f11ba",
                                              },
                                              new CategoryProduct
                                              {
                                                  CategoryId = "c3e985f9-3450-441b-b12a-f922bb5d67b7",
                                                  ProductId = "99214889-61aa-4285-a80b-1ae5281f11ba",
                                              },
                                              new CategoryProduct
                                              {
                                                  CategoryId = "f7cdc372-f45c-486a-83c7-75ba640352e8",
                                                  ProductId = "99214889-61aa-4285-a80b-1ae5281f11b0",
                                              },
                                              new CategoryProduct
                                              {
                                                  CategoryId = "8d36fd4f-0bbc-4fc4-a277-36f1d9657b8f",
                                                  ProductId = "99214889-61aa-4285-a80b-1ae5281f11b0",
                                              },
                                              new CategoryProduct
                                              {
                                                  CategoryId = "c3e985f9-3450-441b-b12a-f922bb5d67b7",
                                                  ProductId = "99214889-61aa-4285-a80b-1ae5281f11b0",
                                              },
                                              new CategoryProduct
                                              {
                                                  CategoryId = "f7cdc372-f45c-486a-83c7-75ba640352e8",
                                                  ProductId = "99214889-61aa-4285-a80b-1ae5281f11bb",
                                              },
                                              new CategoryProduct
                                              {
                                                  CategoryId = "8d36fd4f-0bbc-4fc4-a277-36f1d9657b8f",
                                                  ProductId = "99214889-61aa-4285-a80b-1ae5281f11bb",
                                              },
                                              new CategoryProduct
                                              {
                                                  CategoryId = "c3e985f9-3450-441b-b12a-f922bb5d67b7",
                                                  ProductId = "99214889-61aa-4285-a80b-1ae5281f11bb",
                                              },
                                             };

            await dbContext.CategoriesProducts.AddRangeAsync(initialProductsWithCategories);
        }
    }
}
