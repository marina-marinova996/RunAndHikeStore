using RunAndHikeStore.Web.ViewModels.ShoppingCart;

namespace RunAndHikeStore.Web.ViewModels.Order
{
    using RunAndHikeStore.Data.Models;
    using RunAndHikeStore.Data.Models.Enums;
    using RunAndHikeStore.Web.ViewModels.Brand;
    using RunAndHikeStore.Web.ViewModels.Customer;
    using System;
    using System.Collections.Generic;

    public class CreateOrderViewModel
    {
        public DateTime OrderDate { get; set; } = DateTime.Now;

        public decimal OrderTotalPrice { get; set; }

        public OrderStatus? OrderStatus { get; set; }

        public string CustomerId { get; set; }

        public EditBillingDetailsViewModel BillingDetails { get; set; }

        public IEnumerable<CartItemViewModel> CartItems { get; set; } = new List<CartItemViewModel>();

        public EditAddressViewModel DeliveryAddress { get; set; }

    }
}
