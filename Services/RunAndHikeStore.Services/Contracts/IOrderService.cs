using RunAndHikeStore.Web.ViewModels.ShoppingCart;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RunAndHikeStore.Services.Contracts
{
    public interface IOrderService
    {
        Task CreateAsync(ShoppingCartViewModel model, string customerId);

        Task<IEnumerable<OrderViewModel>> GetAllOrdersAsync(string customerId);
    }
}
