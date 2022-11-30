using RunAndHikeStore.Web.ViewModels.Order;
using System.Collections.Generic;

namespace RunAndHikeStore.Web.ViewModels.Customer
{
    public class CustomerDetailsViewModel
    {
        public string Id { get; set; }

        public AddressViewModel Address { get; set; }

        public IEnumerable<OrderViewModel> Orders { get; set; }

        public EditBillingDetailsViewModel BillingDetails { get; set; }

        public string ShoppingCartId { get; set; }
    }
}
