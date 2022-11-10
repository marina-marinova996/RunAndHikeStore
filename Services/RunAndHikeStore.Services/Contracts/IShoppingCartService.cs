using RunAndHikeStore.Data.Models;
using RunAndHikeStore.Web.ViewModels;
using RunAndHikeStore.Web.ViewModels.ShoppingCart;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RunAndHikeStore.Services.Contracts
{
    public interface IShoppingCartService
    {
        Task AddToCart(string productId, string userId, string sizeId, int quantity);

        Task<ApplicationUser> FindUserById(string userId);

        Task RemoveCartItem(string cartItemId);

        Task RemoveAllCartItems(string userId);

        Task<IEnumerable<CartItemViewModel>> GetAllCartItems(string userId);

        Task<CartItemViewModel> FindCartItem(string productId, string userId);

        Task<CartItem> CreateCartItem(string productId, string userId, string sizeId, int quantity);

        //void BuyProducts(string userId);

        //Task<IEnumerable<ShoppingCartViewModel>> GetProducts(string userId);
    }
}
