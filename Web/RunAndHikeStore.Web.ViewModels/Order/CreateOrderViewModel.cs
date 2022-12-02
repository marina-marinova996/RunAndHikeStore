using RunAndHikeStore.Web.ViewModels.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunAndHikeStore.Web.ViewModels.Order
{
    public class CreateOrderViewModel : OrderViewModel
    {
        public EditBillingDetailsViewModel BillingDetails { get; set; }

        public AddressViewModel DeliveryAddress { get; set; }
    }
}
