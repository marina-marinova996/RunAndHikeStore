using RunAndHikeStore.Data.Common.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RunAndHikeStore.Common.GlobalConstants.Address;
using static RunAndHikeStore.Common.GlobalConstants.ApplicationUser;

namespace RunAndHikeStore.Data.Models
{
    public class BillingDetails : BaseDeletableModel<string>
    {
        public BillingDetails()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Recipient First Name.
        /// </summary>
        [Required]
        [StringLength(FirstNameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(LastNameMaxLength)]
        /// <summary>
        /// Recipient Last Name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Recipient Street Address.
        /// </summary>
        [StringLength(StreetAddressMaxLength)]
        public string StreetAddress { get; set; }

        /// <summary>
        /// Recipient City.
        /// </summary>
        [Required]
        [StringLength(CityMaxLength)]
        public string City { get; set; }

        /// <summary>
        /// Recipient Country.
        /// </summary>
        [Required]
        [StringLength(CountryMaxLength)]
        public string Country { get; set; }

        /// <summary>
        /// Postal code of the address.
        /// </summary>
        [Required]
        [StringLength(PostalCodeMaxLength)]
        public string PostalCode { get; set; }

        /// <summary>
        /// Recipient Phone Number.
        /// </summary>
        [Required]
        [StringLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Foreign Key to ApplicationUsers Table.
        /// </summary>
        [Required]
        public string CustomerId { get; set; }

        /// <summary>
        /// Navigation property to ApplicationUsers Table.
        /// </summary>
        [ForeignKey("CustomerId")]
        public ApplicationUser Customer { get; set; }
    }
}
