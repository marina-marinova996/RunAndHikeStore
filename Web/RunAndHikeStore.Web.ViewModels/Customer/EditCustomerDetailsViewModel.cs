using RunAndHikeStore.Web.ViewModels.Order;
using System.Collections.Generic;

namespace RunAndHikeStore.Web.ViewModels.Customer
{
    public class EditCustomerDetailsViewModel
    {
        /// <summary>
        /// Delivery Address.
        /// </summary>
        public EditAddressViewModel Address { get; set; }

        /// <summary>
        /// Billing Details.
        /// </summary>
        public EditBillingDetailsViewModel BillingDetails { get; set; }
    }
}
