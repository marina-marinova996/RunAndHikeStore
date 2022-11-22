namespace RunAndHikeStore.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static RunAndHikeStore.Common.GlobalConstants.ApplicationUser;
    using static RunAndHikeStore.Common.GlobalConstants.Address;

    using RunAndHikeStore.Data.Common.Models;

    public class OrderDetail : BaseModel<string>
    {
        public OrderDetail()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Foreign key to Orders Table.
        /// </summary>
        [Required]
        public string OrderId { get; set; }

        /// <summary>
        /// Navigation Property to Orders Table.
        /// </summary>
        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; }

        /// <summary>
        /// Gets or sets the Order Quantity.
        /// </summary>
        [Required]
        public int OrderQuantity { get; set; }

        /// <summary>
        /// Foreign key to Products Table.
        /// </summary>
        [Required]
        public string ProductId { get; set; }

        /// <summary>
        /// Navigation Property to Products Table.
        /// </summary>
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

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
    }
}
