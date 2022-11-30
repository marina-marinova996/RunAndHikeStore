namespace RunAndHikeStore.Web.ViewModels.Order
{
    using RunAndHikeStore.Web.ViewModels.Customer;
    using RunAndHikeStore.Web.ViewModels.ShoppingCart;
    using System.Collections.Generic;

    public class OrderDetailFormViewModel
    {

        public IEnumerable<CartItemViewModel> CartItems { get; set; }

        public BillingDetailsFormViewModel BillingDetails { get; set; }

        public AddressViewModel DeliveryAddress { get; set; }
    }
}
