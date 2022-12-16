namespace RunAndHikeStore.Services
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using RunAndHikeStore.Data.Common.Repositories;
    using RunAndHikeStore.Data.Models;
    using RunAndHikeStore.Data.Models.Enums;
    using RunAndHikeStore.Services.Contracts;
    using RunAndHikeStore.Web.ViewModels.Stock;

    public class StockService : IStockService
    {
        private readonly IRepository repo;

        /// <summary>
        /// IoC.
        /// </summary>
        /// <param name="repo"></param>
        public StockService(
            IRepository repo)
        {
            this.repo = repo;
        }

        /// <summary>
        /// Add stock for a product.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task AddStock(AddStockViewModel model)
        {
            if (!(await this.ExistsById(model.ProductId, model.SizeId)))
            {
                var product = await this.repo.All<Product>()
                             .Where(p => p.Id == model.ProductId)
                             .Where(p => p.IsDeleted == false)
                             .Include(p => p.Sizes)
                             .FirstOrDefaultAsync();

                var size = product.Sizes.Where(p => p.SizeId == model.SizeId).FirstOrDefault();

                if (!product.Sizes.Any(ps => ps.SizeId == model.SizeId))
                {
                    product.Sizes.Add(new ProductSize()
                    {
                        ProductId = model.ProductId,
                        SizeId = model.SizeId,
                        UnitsInStock = model.UnitsInStock,
                    });

                    await this.repo.SaveChangesAsync();
                }
            }
            else
            {

               throw new ArgumentException("There is already added stock for this product and size");
            }
        }

        /// <summary>
        /// Delete stock for a product.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task DeleteStock(string productId, string sizeId)
        {
            var product = await this.repo.All<Product>()
                                         .Where(p => p.IsDeleted == false)
                                         .Where(p => p.Id == productId)
                                         .Include(p => p.Sizes)
                                         .FirstOrDefaultAsync();

            var size = product.Sizes.Where(s => s.SizeId == sizeId)
                                    .FirstOrDefault();

            if (product == null)
            {
                throw new ArgumentException("Invalid product ID");
            }

            if (size != null)
            {
                var productSize = product.Sizes
                                         .Where(s => s.IsDeleted == false)
                                         .Where(s => s.SizeId == sizeId)
                                         .FirstOrDefault();

                productSize.IsDeleted = true;

                await this.repo.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Edit stock for a product.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task EditStock(EditStockViewModel model)
        {
            var productSize = await this.repo.All<ProductSize>()
                                           .Where(ps => ps.IsDeleted == false)
                                           .Where(ps => ps.ProductId == model.ProductId)
                                           .Where(ps => ps.SizeId == model.SizeId)
                                           .FirstOrDefaultAsync();
            if (productSize == null)
            {
                throw new ArgumentException("Invalid product ID");
            }

            productSize.UnitsInStock = model.UnitsInStock;

            await this.repo.SaveChangesAsync();
        }

        /// <summary>
        /// Get Add Stock View Model by product Id.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<AddStockViewModel> GetStockViewModelByProductId(string productId)
        {
            var product = await this.repo.All<Product>()
                                         .Where(p => p.IsDeleted == false)
                                         .Where(p => p.Id == productId)
                                         .Include(p => p.Brand)
                                         .Include(p => p.ProductType)
                                         .Where(p => p.Brand.IsDeleted == false)
                                         .Where(p => p.ProductType.IsDeleted == false)
                                         .FirstOrDefaultAsync();

            var model = new AddStockViewModel()
            {
                ProductId = productId,
                Brand = product.Brand.Name,
                Name = product.Name,
                ProductType = product.ProductType.Name,
                ProductNumber = product.ProductNumber,
                Color = product.Color,
                Gender = GetGenderAsStringById((int)product.Gender),
                UnitPrice = product.UnitPrice,
            };

            return model;
        }

        /// <summary>
        /// Get Gender As String By Id - helper method.
        /// </summary>
        /// <param name="genderId"></param>
        /// <returns></returns>
        public static string GetGenderAsStringById(int genderId)
        {
            switch (genderId)
            {
                case (int)Gender.Male:
                    return "Male";
                    break;
                case (int)Gender.Female:
                    return "Female";
                    break;
                case (int)Gender.Unisex:
                    return "Unisex";
                    break;
            }

            return "Not specified";
        }

        /// <summary>
        /// Get all stocks for all products.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<AllStocksViewModel> GetAllStocksAsync(string searchTerm, int currentPage = 1, int productsPerPage = 6)
        {
            var stocksQuery = this.repo.AsNoTracking<ProductSize>()
                                                .Where(ps => ps.IsDeleted == false)
                                                .Include(ps => ps.Size)
                                                .Include(ps => ps.Product)
                                                .ThenInclude(p => p.ProductType)
                                                .Include(ps => ps.Product.Brand)
                                                .Where(ps => ps.Product.IsDeleted == false)
                                                .Where(ps => ps.Product.ProductType.IsDeleted == false)
                                                .Where(ps => ps.Product.IsDeleted == false)
                                                .Where(ps => ps.Size.IsDeleted == false)
                                                .Where(ps => ps.Product.Brand.IsDeleted == false)
                                                .AsQueryable();

            if (stocksQuery == null)
            {
                throw new ArgumentException("No products in stock");
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                stocksQuery = stocksQuery.Where(s => s.Product.Name.ToLower().Contains(searchTerm.ToLower()) ||
                                                    s.Product.ProductNumber.ToLower().Contains(searchTerm.ToLower()) ||
                                                    s.Size.Name.ToLower().Contains(searchTerm.ToLower()) ||
                                                    s.Product.ProductType.Name.ToLower().Contains(searchTerm.ToLower()) ||
                                                    s.Product.Brand.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            var stocks = stocksQuery
                                    .Skip((currentPage - 1) * productsPerPage)
                                    .Take(productsPerPage)
                                    .Select(p => new EditStockViewModel()
                                    {
                                        ProductId = p.ProductId,
                                        ProductNumber = p.Product.ProductNumber,
                                        Name = p.Product.Name,
                                        Color = p.Product.Color,
                                        ProductType = p.Product.ProductType.Name,
                                        Brand = p.Product.Brand.Name,
                                        Gender = GetGenderAsStringById((int)p.Product.Gender),
                                        UnitPrice = p.Product.UnitPrice,
                                        SizeId = p.SizeId,
                                        SizeName = p.Size.Name,
                                        UnitsInStock = p.UnitsInStock,
                                    });

            var totalRecords = stocksQuery.Count();

            return new AllStocksViewModel()
            {
                Stocks = stocks,
                TotalRecordsCount = totalRecords,
            };
        }

        /// <summary>
        /// Get view model for edit.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="sizeId"></param>
        /// <returns></returns>
        public async Task<EditStockViewModel> GetViewModelForEdit(string productId, string sizeId)
        {
            var modelForEdit = await this.repo.All<ProductSize>()
                                              .Where(ps => ps.IsDeleted == false)
                                              .Include(ps => ps.Size)
                                              .Where(ps => ps.Size.IsDeleted == false)
                                              .Include(ps => ps.Product)
                                              .ThenInclude(p => p.Brand)
                                              .Where(ps => ps.Product.IsDeleted == false)
                                              .Where(ps => ps.ProductId == productId)
                                              .Where(p => p.SizeId == sizeId)
                                              .Select(ps => new EditStockViewModel()
                                              {
                                                  ProductId = ps.ProductId,
                                                  ProductNumber = ps.Product.ProductNumber,
                                                  Name = ps.Product.Name,
                                                  Color = ps.Product.Color,
                                                  ProductType = ps.Product.ProductType.Name,
                                                  Brand = ps.Product.Brand.Name,
                                                  Gender = GetGenderAsStringById((int)ps.Product.Gender),
                                                  SizeId = ps.SizeId,
                                                  SizeName = ps.Size.Name,
                                                  UnitsInStock = ps.UnitsInStock,
                                              })
                                              .FirstOrDefaultAsync();

            return modelForEdit;
        }

        /// <summary>
        /// Get all stocks.
        /// </summary>
        /// <returns></returns>
        public async Task<List<ProductSize>> GetAllStocks()
        {
            return await this.repo.All<ProductSize>().ToListAsync();
        }

        /// <summary>
        /// Check if stock exists.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="sizeId"></param>
        /// <returns></returns>
        public async Task<bool> ExistsById(string productId, string sizeId)
        {
            return await repo.All<ProductSize>()
                             .AnyAsync(ps => ps.ProductId == productId && ps.SizeId == sizeId);
        }
    }
}
