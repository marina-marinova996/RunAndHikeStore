using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunAndHikeStore.Web.ViewModels.Customer
{
    public class CustomerOrderViewModel
    {
        public string OrderId { get; set; }

        public int OrderQuantity { get; set; }

        public string OrderNumber { get; set; }

        public string OrderStatus { get; set; }

        public string TotalPrice { get; set; }

        public string OrderDate { get; set; }

        public BillingDetailsFormViewModel BillingDetails { get; set; }

        public string Email { get; set; }

        public string PaymentStatus { get; set; }
    }
}
