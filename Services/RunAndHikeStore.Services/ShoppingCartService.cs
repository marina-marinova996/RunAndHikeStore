namespace RunAndHikeStore.Services
{
    using Microsoft.EntityFrameworkCore;
    using RunAndHikeStore.Data.Common.Repositories;
    using RunAndHikeStore.Data.Models;
    using RunAndHikeStore.Services.Contracts;
    using RunAndHikeStore.Web.ViewModels;
    using RunAndHikeStore.Web.ViewModels.ShoppingCart;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository repo;

        public ShoppingCartService(IRepository repo)
        {
            this.repo = repo;
        }

        //public async Task<IEnumerable<ShoppingCartViewModel>> AddProduct(string productId, string userId)
        //{
        //    var user = this.repo.All<ApplicationUser>()
        //                .Where(u => u.Id == userId)
        //                .Include(u => u.ShoppingCart)
        //                .ThenInclude(u => u.Products)
        //                .FirstOrDefault();

        //    var product = this.repo.All<ShoppingCartProduct>()
        //                .FirstOrDefault(shp => shp.ProductId == productId);

        //    user.ShoppingCart.Products.Add(product);

        //    await this.repo.SaveChangesAsync();

        //    return user
        //                .ShoppingCart
        //                .Products
        //                .Select(p => new ShoppingCartViewModel()
        //                {
        //                    Name = p.Product.Name,
        //                    Price = p.Product.UnitPrice.ToString("F2"),
        //                });
        //}

        //public async void BuyProducts(string userId)
        //{
        //    var user = this.repo.All<ApplicationUser>()
        //                    .Where(u => u.Id == userId)
        //                    .Include(u => u.ShoppingCart)
        //                    .ThenInclude(s => s.Products)
        //                    .FirstOrDefault();

        //    user.ShoppingCart.Products.Clear();

        //    await this.repo.SaveChangesAsync();
        //}

        //public async Task<IEnumerable<ShoppingCartViewModel>> GetProducts(string userId)
        //{
        //    var user = await this.repo.All<ApplicationUser>()
        //                    .Where(u => u.Id == userId)
        //                    .Include(u => u.ShoppingCart)
        //                    .ThenInclude(s => s.Products)
        //                    .FirstOrDefaultAsync();

        //    return user
        //               .ShoppingCart
        //               .Products
        //               .Select(p => new ShoppingCartViewModel()
        //                {
        //                    ProductName = p.Product.Name,
        //                    ProductPrice = p.Price.ToString("F2"),
        //                });
        //}
    }
}
