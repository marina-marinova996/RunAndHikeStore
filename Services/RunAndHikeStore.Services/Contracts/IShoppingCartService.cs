using RunAndHikeStore.Web.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RunAndHikeStore.Services.Contracts
{
    public interface IShoppingCartService
    {
        Task<IEnumerable<ShoppingCartViewModel>> AddProduct(string productId, string userId);

        void BuyProducts(string userId);

        Task<IEnumerable<ShoppingCartViewModel>> GetProducts(string userId);
    }
}
