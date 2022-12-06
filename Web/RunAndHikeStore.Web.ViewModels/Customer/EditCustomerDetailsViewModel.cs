using RunAndHikeStore.Web.ViewModels.Order;
using System.Collections.Generic;

namespace RunAndHikeStore.Web.ViewModels.Customer
{
    public class EditCustomerDetailsViewModel
    {
        public EditAddressViewModel Address { get; set; }

        public EditBillingDetailsViewModel BillingDetails { get; set; }
    }
}
