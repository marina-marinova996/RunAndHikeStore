using RunAndHikeStore.Web.ViewModels.Customer;
using RunAndHikeStore.Web.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunAndHikeStore.Web.ViewModels.ShoppingCart
{
    public class ShoppingCartViewModel
    {
        public IEnumerable<CartItemViewModel> CartItems { get; set; }

        public CreateOrderViewModel Order { get; set; }
    }
}
