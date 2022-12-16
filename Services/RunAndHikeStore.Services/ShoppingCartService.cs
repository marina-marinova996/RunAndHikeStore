namespace RunAndHikeStore.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using RunAndHikeStore.Data.Common.Repositories;
    using RunAndHikeStore.Data.Models;
    using RunAndHikeStore.Services.Contracts;
    using RunAndHikeStore.Web.ViewModels.Product;
    using RunAndHikeStore.Web.ViewModels.ShoppingCart;

    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository repo;

        public ShoppingCartService(IRepository repo)
        {
            this.repo = repo;
        }

        /// <summary>
        /// Add Item to Cart.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="userId"></param>
        /// <param name="sizeId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public async Task AddToCart(string productId, string userId, string sizeId, int quantity)
        {
            var user = await this.FindUserById(userId);

            bool isCreated = user.ShoppingCart.CartItems.Any(c => c.ProductId == productId && c.SizeId == sizeId);

            if (await this.IsInStock(productId, sizeId))
            {
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
            else
            {
                throw new ArgumentException("Not enough units in stock");
            }
        }

        /// <summary>
        /// Counter for Shopping Cart Items.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<int> CountShoppingCartItemsQuantity(string userId)
        {
            var user = await this.FindUserById(userId);

            var quantity = 0;

            foreach (var count in user.ShoppingCart.CartItems)
            {
                quantity += count.Quantity;
            }

            return quantity;
        }

        /// <summary>
        /// Create new cart item.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="userId"></param>
        /// <param name="sizeId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Find Cart Item.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<CartItemViewModel> FindCartItem(string productId, string userId)
        {
            return await this.repo.AsNoTracking<CartItem>()
                                  .Include(c => c.ShoppingCart)
                                  .Include(c => c.ShoppingCart.ApplicationUser)
                                  .Where(c => c.ShoppingCart.ApplicationUser.Id == userId)
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

        /// <summary>
        /// Find user by id.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<ApplicationUser> FindUserById(string userId)
        {
            return await this.repo.All<ApplicationUser>()
                                      .Where(u => u.Id == userId)
                                      .Include(u => u.ShoppingCart)
                                      .ThenInclude(sh => sh.CartItems)
                                      .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Get all cart items for user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CartItemViewModel>> GetAllCartItems(string userId)
        {
            return await this.repo.AsNoTracking<CartItem>()
                             .Include(c => c.ShoppingCart)
                             .Include(c => c.ShoppingCart.ApplicationUser)
                             .Where(c => c.ShoppingCart.ApplicationUser.IsDeleted == false)
                             .Where(c => c.ShoppingCart.ApplicationUser.Id == userId)
                             .Include(c => c.Product)
                             .Where(c => c.Product.IsDeleted == false)
                             .Include(c => c.Size)
                             .Select(c => new CartItemViewModel
                             {
                                 Id = c.Id,
                                 ShoppingCartId = c.ShoppingCartId,
                                 Quantity = c.Quantity,
                                 ProductId = c.ProductId,
                                 ApplicationUserId = c.ShoppingCart.ApplicationUser.Id,
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

        /// <summary>
        /// Check if product is in stock.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="sizeId"></param>
        /// <returns></returns>
        public async Task<bool> IsInStock(string productId, string sizeId)
        {
            var cartItems = await this.repo.AsNoTracking<CartItem>().Where(c => c.ProductId == productId && c.SizeId == sizeId).ToListAsync();

            var quantityInCartItems = cartItems.Select(x => x.Quantity).ToList().Sum();

            var isInStock = await this.repo.AsNoTracking<ProductSize>()
                                  .Where(ps => ps.ProductId == productId && ps.SizeId == sizeId)
                                  .AnyAsync(ps => ps.UnitsInStock - quantityInCartItems > 0);

            return isInStock;
        }

        /// <summary>
        /// Remove all cart items.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task RemoveAllCartItems(string userId)
        {
            var allCartItems = await this.repo.All<CartItem>()
                                              .Include(c => c.ShoppingCart)
                                              .Where(c => c.ShoppingCart.ApplicationUser.Id == userId)
                                              .ToListAsync();

            this.repo.DeleteRange(allCartItems);

            await this.repo.SaveChangesAsync();
        }

        /// <summary>
        /// Remove cart item by id.
        /// </summary>
        /// <param name="cartItemId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Check if CartItem exists.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> ExistsCartItemById(string cartItemId)
        {
            return await repo.All<CartItem>()
                            .AnyAsync(c => c.Id == cartItemId);
        }
    }
}
