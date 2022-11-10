using RunAndHikeStore.Data.Common.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RunAndHikeStore.Data.Models
{
    public class CartItem : BaseModel<string>
    {
        public CartItem()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Foreign key to ShoppingCarts table.
        /// </summary>
        [Required]
        [ForeignKey(nameof(ShoppingCartId))]
        public string ShoppingCartId { get; set; }

        /// <summary>
        /// Navigation property to ShoppingCarts table.
        /// </summary>
        public ShoppingCart ShoppingCart { get; set; }

        /// <summary>
        /// Product quantity in the cart.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Foreign key to Products table.
        /// </summary>
        [Required]
        public string ProductId { get; set; }

        /// <summary>
        /// Navigation property to Products table.
        /// </summary>
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        /// <summary>
        /// Foreign key to Sizes table.
        /// </summary>
        [Required]
        public string SizeId { get; set; }
        /// <summary>
        /// Navigation property to Sizes table.
        /// </summary>
        [ForeignKey(nameof(SizeId))]
        public Size Size { get; set; }
    }
}
