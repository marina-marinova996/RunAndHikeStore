using RunAndHikeStore.Data.Models;
using RunAndHikeStore.Web.ViewModels.ShoppingCart;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RunAndHikeStore.Services.Contracts
{
    public interface IShoppingCartService
    {
        /// <summary>
        /// Add product to cart.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="userId"></param>
        /// <param name="sizeId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        Task AddToCart(string productId, string userId, string sizeId, int quantity);

        /// <summary>
        /// Find user by id.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<ApplicationUser> FindUserById(string userId);

        /// <summary>
        /// Check if product is in stock.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="sizeId"></param>
        /// <returns></returns>
        Task<bool> IsInStock(string productId, string sizeId);

        /// <summary>
        /// Remove cart item.
        /// </summary>
        /// <param name="cartItemId"></param>
        /// <returns></returns>
        Task RemoveCartItem(string cartItemId);

        /// <summary>
        /// Remove all cart items.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task RemoveAllCartItems(string userId);

        /// <summary>
        /// Get all cart items.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<CartItemViewModel>> GetAllCartItems(string userId);

        /// <summary>
        /// Find cart item.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<CartItemViewModel> FindCartItem(string productId, string userId);

        /// <summary>
        /// Create Cart Item.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="userId"></param>
        /// <param name="sizeId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        Task<CartItem> CreateCartItem(string productId, string userId, string sizeId, int quantity);

        /// <summary>
        /// Count Shopping cart items.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<int> CountShoppingCartItemsQuantity(string userId);

        /// <summary>
        /// Check if CartItem exists.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> ExistsCartItemById(string cartItemId);

    }
}
