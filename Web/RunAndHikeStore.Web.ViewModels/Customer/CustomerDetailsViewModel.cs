namespace RunAndHikeStore.Web.ViewModels.Customer
{
    public class CustomerDetailsViewModel
    {
        /// <summary>
        /// Delivery Address.
        /// </summary>
        public AddressViewModel Address { get; set; }

        /// <summary>
        /// Billing Details.
        /// </summary>
        public BillingDetailsFormViewModel BillingDetails { get; set; }
    }
}
