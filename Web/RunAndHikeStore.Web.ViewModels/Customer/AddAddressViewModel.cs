using RunAndHikeStore.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;
using static RunAndHikeStore.Common.GlobalConstants.Address;

namespace RunAndHikeStore.Web.ViewModels.Customer
{
    public class AddAddressViewModel
    {
        [Required]
        [StringLength(StreetAddressMaxLength, MinimumLength = StreetAddressMinLength)]
        public string StreetAddress { get; set; }

        [Required]
        [StringLength(CityMaxLength, MinimumLength = CityMinLength)]
        public string City { get; set; }

        [Required]
        [StringLength(CountryMaxLength, MinimumLength = CountryMinLength)]
        public string Country { get; set; }

        [Required]
        [StringLength(PostalCodeMaxLength, MinimumLength = PostalCodeMinLength)]
        public string PostalCode { get; set; }

        [Required]
        public AddressType AddressType { get; set; }
    }
}
