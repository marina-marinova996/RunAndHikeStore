using RunAndHikeStore.Web.ViewModels.Customer;
using RunAndHikeStore.Web.ViewModels.Product;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RunAndHikeStore.Web.ViewModels.Order
{
    public class EditOrderDetailViewModel
    {
        public int OrderId { get; set; }

        public string OrderDate { get; set; }

        public string Email { get; set; }

        public string TotalPrice { get; set; }

        [Required]
        public int OrderStatusId { get; set; }

        public IEnumerable<OrderStatusViewModel> OrderStatuses { get; set; }

        [Required]
        public int PaymentStatusId { get; set; }

        public IEnumerable<PaymentStatusViewModel> PaymentStatuses { get; set; }

        public EditBillingDetailsViewModel BillingDetails { get; set; }
    }
}
