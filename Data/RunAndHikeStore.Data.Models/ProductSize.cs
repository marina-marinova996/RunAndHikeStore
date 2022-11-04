using RunAndHikeStore.Data.Common.Models;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RunAndHikeStore.Common.GlobalConstants;

namespace RunAndHikeStore.Data.Models
{
    public class ProductSize
    {
        /// <summary>
        /// Foreign Key Table Sizes. Part of composite primary key.
        /// </summary>
        [Required]
        public string SizeId { get; set; }

        /// <summary>
        /// Foreign Key Table Products.Part of composite primary key.
        /// </summary>
        [Required]
        public string ProductId { get; set; }

        /// <summary>
        /// Navigation property to Sizes table.
        /// </summary>
        [ForeignKey(nameof(SizeId))]
        public Size Size { get; set; }

        /// <summary>
        /// Navigation property to Products table.
        /// </summary>
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        /// <summary>
        /// Gets or sets the units in stock of the product.
        /// </summary>
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Units in stock must be positive number.")]
        public int UnitsInStock { get; set; }

        public bool IsDeleted { get; set; }
    }
}
