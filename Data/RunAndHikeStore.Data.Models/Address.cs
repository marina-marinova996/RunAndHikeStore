namespace RunAndHikeStore.Data.Models
{
    using RunAndHikeStore.Data.Common.Models;
    using RunAndHikeStore.Data.Models.Enums;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Address : BaseDeletableModel<string>
    {
        public Address()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Street of the address.
        /// </summary>
        [Required]
        [StringLength(150)]
        public string StreetAddress { get; set; }

        /// <summary>
        /// City of the address.
        /// </summary>
        [Required]
        [StringLength(200)]
        public string City { get; set; }

        /// <summary>
        /// Country of the address.
        /// </summary>
        [Required]
        [StringLength(70)]
        public string Country { get; set; }

        /// <summary>
        /// Postal code of the address.
        /// </summary>
        [Required]
        [StringLength(20)]
        public string PostalCode { get; set; }
        /// <summary>
        /// Type of the address.
        /// </summary>
        [Required]
        [StringLength(20)]
        public AddressType AddressType { get; set; }
    }
}
