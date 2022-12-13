using System.ComponentModel.DataAnnotations;
using static RunAndHikeStore.Common.GlobalConstants.Address;
using static RunAndHikeStore.Common.GlobalConstants.ApplicationUser;

namespace RunAndHikeStore.Web.ViewModels.Customer
{
    public class BillingDetailsFormViewModel
    {
        /// <summary>
        /// First Name.
        /// </summary>
        [StringLength(FirstNameMaxLength, MinimumLength = FirstNameMinLength)]
        public string FirstName { get; set; }

        /// <summary>
        /// Last Name.
        /// </summary>
        [StringLength(LastNameMaxLength, MinimumLength = LastNameMinLength)]
        public string LastName { get; set; }

        /// <summary>
        /// Street Address.
        /// </summary>
        [Required]
        [StringLength(StreetAddressMaxLength, MinimumLength = StreetAddressMinLength)]
        public string StreetAddress { get; set; }

        /// <summary>
        /// City.
        /// </summary>
        [Required]
        [StringLength(CityMaxLength, MinimumLength = CityMinLength)]
        public string City { get; set; }

        /// <summary>
        /// Country.
        /// </summary>
        [Required]
        [StringLength(CountryMaxLength, MinimumLength = CountryMinLength)]
        public string Country { get; set; }

        /// <summary>
        /// Postal Code.
        /// </summary>
        [Required]
        [StringLength(PostalCodeMaxLength, MinimumLength = PostalCodeMinLength)]
        public string PostalCode { get; set; }

        /// <summary>
        /// Phone Number.
        /// </summary>
        [Required]
        [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength)]
        public string PhoneNumber { get; set; }
    }
}
