using RunAndHikeStore.Web.ViewModels.ShoppingCart;

namespace RunAndHikeStore.Web.ViewModels.Order
{
    using RunAndHikeStore.Web.ViewModels.Customer;
    using System.Collections.Generic;

    public class CreateOrderViewModel
    {
        public EditBillingDetailsViewModel BillingDetails { get; set; }

        public IEnumerable<CartItemViewModel> CartItems { get; set; } = new List<CartItemViewModel>();

        public EditAddressViewModel DeliveryAddress { get; set; }

    }
}
