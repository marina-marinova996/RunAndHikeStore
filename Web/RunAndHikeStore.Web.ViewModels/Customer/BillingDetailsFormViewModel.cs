using System.ComponentModel.DataAnnotations;
using static RunAndHikeStore.Common.GlobalConstants.Address;
using static RunAndHikeStore.Common.GlobalConstants.ApplicationUser;

namespace RunAndHikeStore.Web.ViewModels.Customer
{
    public class BillingDetailsFormViewModel
    {
        [StringLength(FirstNameMaxLength, MinimumLength = FirstNameMinLength)]
        public string FirstName { get; set; }

        [StringLength(LastNameMaxLength, MinimumLength = LastNameMinLength)]
        public string LastName { get; set; }

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
        [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength)]
        public string PhoneNumber { get; set; }
    }
}
