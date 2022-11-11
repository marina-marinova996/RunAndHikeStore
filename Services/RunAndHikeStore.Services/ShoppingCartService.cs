namespace RunAndHikeStore.Services
{
    using Microsoft.EntityFrameworkCore;
    using RunAndHikeStore.Data.Common.Repositories;
    using RunAndHikeStore.Data.Models;
    using RunAndHikeStore.Services.Contracts;
    using RunAndHikeStore.Web.ViewModels.Product;
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

        public async Task AddToCart(string productId, string userId, string sizeId, int quantity)
        {
            var user = await this.FindUserById(userId);

            bool isCreated = user.ShoppingCart.CartItems.Any(c => c.ProductId == productId && c.SizeId == sizeId);

            if (!isCreated)
            {
                var cartItem = await this.CreateCartItem(productId, userId, sizeId, quantity);
                user.ShoppingCart.CartItems.Add(cartItem);
            }
            else
            {
                var cartItem = user.ShoppingCart.CartItems
                                        .Where(c => c.ProductId == productId && c.SizeId == sizeId)
                                        .FirstOrDefault();
                cartItem.Quantity++;
            }

            await this.repo.SaveChangesAsync();
        }

        public async Task<CartItem> CreateCartItem(string productId, string userId, string sizeId, int quantity)
        {
            var user = await this.FindUserById(userId);

            var cartItem = new CartItem()
            {
                ProductId = productId,
                ShoppingCartId = user.ShoppingCart.Id,
                SizeId = sizeId,
                Quantity = quantity,
            };

            return cartItem;
        }

        public async Task<CartItemViewModel> FindCartItem(string productId, string userId)
        {
            return await this.repo.AsNoTracking<CartItem>()
                                  .Include(c => c.ShoppingCart)
                                  .Include(c => c.ShoppingCart.ApplicationUser)
                                  .Where(c => c.ShoppingCart.ApplicationUserId == userId)
                                  .Where(c => c.ShoppingCart.ApplicationUser.IsDeleted == false)
                                  .Include(c => c.Product)
                                  .Where(c => c.Product.IsDeleted == false)
                                  .Where(c => c.ProductId == productId)
                                  .Include(c => c.Size)
                                  .Select(c => new CartItemViewModel
                                  {
                                      Id = c.Id,
                                      ShoppingCartId = c.ShoppingCartId,
                                      Quantity = c.Quantity,
                                      ProductId = c.ProductId,
                                      ApplicationUserId = userId,
                                      Product = new ProductViewModel
                                      {
                                          Id = c.Product.Id,
                                          Name = c.Product.Name,
                                          UnitPrice = c.Product.UnitPrice,
                                          ImageUrl = c.Product.ImageUrl,
                                      },
                                      SizeId = c.SizeId,
                                      Size = c.Size.Name,
                                  }).FirstOrDefaultAsync();
        }

        public async Task<ApplicationUser> FindUserById(string userId)
        {
            return await this.repo.All<ApplicationUser>()
                                      .Where(u => u.Id == userId)
                                      .Include(u => u.ShoppingCart)
                                      .ThenInclude(sh => sh.CartItems)
                                      .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<CartItemViewModel>> GetAllCartItems(string userId)
        {
            return await this.repo.AsNoTracking<CartItem>()
                             .Include(c => c.ShoppingCart)
                             .Include(c => c.ShoppingCart.ApplicationUser)
                             .Where(c => c.ShoppingCart.ApplicationUser.IsDeleted == false)
                             .Where(c => c.ShoppingCart.ApplicationUserId == userId)
                             .Include(c => c.Product)
                             .Where(c => c.Product.IsDeleted == false)
                             .Include(c => c.Size)
                             .Select(c => new CartItemViewModel
                             {
                                 Id = c.Id,
                                 ShoppingCartId = c.ShoppingCartId,
                                 Quantity = c.Quantity,
                                 ProductId = c.ProductId,
                                 ApplicationUserId = c.ShoppingCart.ApplicationUserId,
                                 Product = new ProductViewModel
                                 {
                                     Id = c.Product.Id,
                                     ProductNumber = c.Product.ProductNumber,
                                     Name = c.Product.Name,
                                     UnitPrice = c.Product.UnitPrice,
                                     ImageUrl = c.Product.ImageUrl,
                                 },
                                 SizeId = c.SizeId,
                                 Size = c.Size.Name,
                                 Total = (c.Quantity * c.Product.UnitPrice).ToString("f2"),
                             }).ToListAsync();
        }

        public async Task<bool> IsInStock(string productId, string sizeId)
        {
            return await this.repo.AsNoTracking<ProductSize>()
                                  .Where(ps => ps.ProductId == productId && ps.SizeId == sizeId)
                                  .AnyAsync(ps => ps.UnitsInStock > 0);
        }

        public async Task RemoveAllCartItems(string userId)
        {
            var allCartItems = await this.repo.All<CartItem>()
                                              .Include(c => c.ShoppingCart)
                                              .Where(c => c.ShoppingCart.ApplicationUserId == userId)
                                              .ToListAsync();

            this.repo.DeleteRange(allCartItems);

            await this.repo.SaveChangesAsync();
        }

        //public async Task<ShoppingCartViewModel> GetShoppingCartByUserId(string userId)
        //{
        //    return await this.repo.AsNoTracking<ShoppingCart>()
        //                    .Where(c => c.ApplicationUserId == userId)
        //                    .Select(c => new ShoppingCartViewModel
        //                    {
        //                        Id = c.Id,
        //                        Appli
        //                    })
        //                    .FirstOrDefaultAsync();

        //}

        public async Task RemoveCartItem(string cartItemId)
        {
            var cartItem = await this.repo.All<CartItem>()
                             .FirstOrDefaultAsync(c => c.Id == cartItemId);

            if (cartItem != null)
            {
                this.repo.Delete(cartItem);
                await this.repo.SaveChangesAsync();
            }
        }

        //public async void BuyProducts(string userId)
        //{
        //    var user = this.repo.All<ApplicationUser>()
        //                    .Where(u => u.Id == userId)
        //                    .Include(u => u.ShoppingCart)
        //                    .ThenInclude(s => s.Products)
        //                    .FirstOrDefault();

        //    user.ShoppingCart.CartItems.Clear();

        //    await this.repo.SaveChangesAsync();
        //}

        //public async Task<ShoppingCartViewModel> GetProducts(string userId)
        //{
        //    var user = await this.repo.All<ApplicationUser>()
        //                    .Where(u => u.Id == userId)
        //                    .Include(u => u.ShoppingCart)
        //                    .ThenInclude(s => s.CartItems)
        //                    .FirstOrDefaultAsync();

        //    return user
        //               .ShoppingCart
        //               .Select(c => new ShoppingCartViewModel()
        //               {
        //                   ProductName = p.Product.Name,
        //                   ProductPrice = p.Price.ToString("F2"),
        //               });
        //}
    }
}
