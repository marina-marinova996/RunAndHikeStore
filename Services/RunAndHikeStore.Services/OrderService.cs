using RunAndHikeStore.Services.Contracts;
using RunAndHikeStore.Web.ViewModels.ShoppingCart;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RunAndHikeStore.Services
{
    public class OrderService : IOrderService
    {
        public Task CreateAsync(ShoppingCartViewModel model, string customerId)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<OrderViewModel>> GetAllOrdersAsync(string customerId)
        {
            throw new System.NotImplementedException();
        }
    }
}
