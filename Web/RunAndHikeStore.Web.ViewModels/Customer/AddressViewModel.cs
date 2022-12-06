using RunAndHikeStore.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;
using static RunAndHikeStore.Common.GlobalConstants.Address;

namespace RunAndHikeStore.Web.ViewModels.Customer
{
    public class AddressViewModel
    {
        [StringLength(StreetAddressMaxLength, MinimumLength = StreetAddressMinLength)]
        public string StreetAddress { get; set; }

        [StringLength(CityMaxLength, MinimumLength = CityMinLength)]
        public string City { get; set; }

        [StringLength(CountryMaxLength, MinimumLength = CountryMinLength)]
        public string Country { get; set; }

        [StringLength(PostalCodeMaxLength, MinimumLength = PostalCodeMinLength)]
        public string PostalCode { get; set; }
    }
}
