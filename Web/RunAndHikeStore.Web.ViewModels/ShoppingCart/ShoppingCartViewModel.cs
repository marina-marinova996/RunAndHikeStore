using RunAndHikeStore.Web.ViewModels.Order;
using System.Collections.Generic;

namespace RunAndHikeStore.Web.ViewModels.ShoppingCart
{
    public class ShoppingCartViewModel
    {
        /// <summary>
        /// Cart Items.
        /// </summary>
        public IEnumerable<CartItemViewModel> CartItems { get; set; }

        public CreateOrderViewModel Order { get; set; }
    }
}
