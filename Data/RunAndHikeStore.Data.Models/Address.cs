﻿namespace RunAndHikeStore.Data.Models
{
    using RunAndHikeStore.Data.Common.Models;
    using RunAndHikeStore.Data.Models.Enums;
    using System;
    using System.ComponentModel.DataAnnotations;
    using static RunAndHikeStore.Common.GlobalConstants.Address;

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
        [StringLength(StreetAddressMaxLength)]
        public string StreetAddress { get; set; }

        /// <summary>
        /// City of the address.
        /// </summary>
        [Required]
        [StringLength(CityMaxLength)]
        public string City { get; set; }

        /// <summary>
        /// Country of the address.
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
        /// Type of the address.
        /// </summary>
        [Required]
        [StringLength(AddressTypeMaxLength)]
        public AddressType AddressType { get; set; }
    }
}
