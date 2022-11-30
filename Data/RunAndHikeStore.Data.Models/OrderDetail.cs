namespace RunAndHikeStore.Data.Models
{
    using Microsoft.EntityFrameworkCore;
    using RunAndHikeStore.Data.Common.Models;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static RunAndHikeStore.Common.GlobalConstants.Size;

    public class OrderDetail : BaseDeletableModel<string>
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
        /// Gets or sets the Unit Price.
        /// </summary>
        [Required]
        [Precision(18, 2)]
        public decimal UnitPrice { get; set; }
        /// <summary>
        /// Gets or sets the Size.
        /// </summary>
        [Required]
        [StringLength(ProductSizeNameMaxLength)]
        public string Size { get; set; }

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
    }
}
