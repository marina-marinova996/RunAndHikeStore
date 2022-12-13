using RunAndHikeStore.Web.ViewModels.ShoppingCart;

namespace RunAndHikeStore.Web.ViewModels.Order
{
    using RunAndHikeStore.Web.ViewModels.Customer;
    using System.Collections.Generic;

    public class CreateOrderViewModel
    {
        /// <summary>
        /// Billing Details.
        /// </summary>
        public EditBillingDetailsViewModel BillingDetails { get; set; }

        /// <summary>
        /// Cart Items.
        /// </summary>
        public IEnumerable<CartItemViewModel> CartItems { get; set; } = new List<CartItemViewModel>();

        /// <summary>
        /// Delivery Address.
        /// </summary>
        public EditAddressViewModel DeliveryAddress { get; set; }

    }
}
