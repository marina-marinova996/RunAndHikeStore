using RunAndHikeStore.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;
using static RunAndHikeStore.Common.GlobalConstants.Address;

namespace RunAndHikeStore.Web.ViewModels.Customer
{
    public class AddressViewModel
    {
        /// <summary>
        /// Street address.
        /// </summary>
        [StringLength(StreetAddressMaxLength, MinimumLength = StreetAddressMinLength)]
        public string StreetAddress { get; set; }

        /// <summary>
        /// City.
        /// </summary>
        [StringLength(CityMaxLength, MinimumLength = CityMinLength)]
        public string City { get; set; }

        /// <summary>
        /// Country.
        /// </summary>
        [StringLength(CountryMaxLength, MinimumLength = CountryMinLength)]
        public string Country { get; set; }

        /// <summary>
        /// Postal Code.
        /// </summary>
        [StringLength(PostalCodeMaxLength, MinimumLength = PostalCodeMinLength)]
        public string PostalCode { get; set; }
    }
}
