using RunAndHikeStore.Web.ViewModels.ShoppingCart;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RunAndHikeStore.Services.Contracts
{
    public interface IOrderService
    {
        Task CreateAsync(OrderViewModel model, string customerId);

        Task<IEnumerable<OrderViewModel>> GetAllOrdersAsync(string customerId);
    }
}
