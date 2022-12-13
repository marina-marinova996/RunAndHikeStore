namespace RunAndHikeStore.Web.ViewModels.Customer
{
    public class CustomerOrderViewModel
    {
        /// <summary>
        /// Order Id.
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// Order Status.
        /// </summary>
        public string OrderStatus { get; set; }

        /// <summary>
        /// Total Price.
        /// </summary>
        public string TotalPrice { get; set; }

        /// <summary>
        /// Order Date.
        /// </summary>
        public string OrderDate { get; set; }

        /// <summary>
        /// Billing Details.
        /// </summary>
        public BillingDetailsFormViewModel BillingDetails { get; set; }

        /// <summary>
        /// Email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Payment status.
        /// </summary>
        public string PaymentStatus { get; set; }
    }
}
