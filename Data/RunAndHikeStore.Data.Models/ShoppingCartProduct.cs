using RunAndHikeStore.Data.Common.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RunAndHikeStore.Data.Models
{
    public class ShoppingCartProduct
    {
        /// <summary>
        /// FK to Products Table. Part of composite key.
        /// </summary>
        [Required]
        public string ProductId { get; set; }

        /// <summary>
        /// FK to ShoppingCarts Table. Part of composite key.
        /// </summary
        [Required]
        public string ShoppingCartId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        [ForeignKey(nameof(ShoppingCartId))]
        public ShoppingCart ShoppingCart { get; set; }

        /// <summary>
        /// Quantity of products in the shopping cart.
        /// </summary>
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        /// <summary>
        /// Product Price.
        /// </summary>
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
    }
}
