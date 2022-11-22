using RunAndHikeStore.Data.Models.Enums;

namespace RunAndHikeStore.Web.ViewModels.Customer
{
    public class AddressViewModel
    {
        public string Id { get; set; }

        public string StreetAddress { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string PostalCode { get; set; }

        public AddressType AddressType { get; set; }
    }
}
