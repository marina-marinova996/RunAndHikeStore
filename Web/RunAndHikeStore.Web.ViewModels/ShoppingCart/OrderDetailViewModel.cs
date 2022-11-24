namespace RunAndHikeStore.Web.ViewModels.ShoppingCart
{
    using RunAndHikeStore.Web.ViewModels.Product;
    using System.ComponentModel.DataAnnotations;
    using static RunAndHikeStore.Common.GlobalConstants.ApplicationUser;
    using static RunAndHikeStore.Common.GlobalConstants.Address;

    public class OrderDetailViewModel
    {
        public string OrderId { get; set; }

        public int OrderQuantity { get; set; }

        public string ProductId { get; set; }

        public ProductViewModel Product { get; set; }

        [Required]
        [StringLength(FirstNameMaxLength, MinimumLength = FirstNameMinLength )]
        public string FirstName { get; set; }

        [Required]
        [StringLength(LastNameMaxLength, MinimumLength = LastNameMaxLength)]
        public string LastName { get; set; }

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
