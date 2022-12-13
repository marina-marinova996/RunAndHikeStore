using RunAndHikeStore.Web.ViewModels.Customer;
using RunAndHikeStore.Web.ViewModels.Product;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RunAndHikeStore.Web.ViewModels.Order
{
    public class EditOrderDetailViewModel
    {
        /// <summary>
        /// Order Id.
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// Order Date.
        /// </summary>
        public string OrderDate { get; set; }

        /// <summary>
        /// Email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Total Price.
        /// </summary>
        public string TotalPrice { get; set; }

        /// <summary>
        /// Order Status Id.
        /// </summary>
        [Required]
        public int OrderStatusId { get; set; }

        /// <summary>
        /// Order Statuses.
        /// </summary>
        public IEnumerable<OrderStatusViewModel> OrderStatuses { get; set; }

        /// <summary>
        /// Payment status Id.
        /// </summary>
        [Required]
        public int PaymentStatusId { get; set; }

        /// <summary>
        /// Payment Statuses.
        /// </summary>
        public IEnumerable<PaymentStatusViewModel> PaymentStatuses { get; set; }

        /// <summary>
        /// Billing details.
        /// </summary>
        public EditBillingDetailsViewModel BillingDetails { get; set; }
    }
}
