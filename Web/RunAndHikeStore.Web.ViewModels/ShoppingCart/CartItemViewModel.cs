using RunAndHikeStore.Web.ViewModels.Product;
using System;
using System.ComponentModel.DataAnnotations;

namespace RunAndHikeStore.Web.ViewModels.ShoppingCart
{
    public class CartItemViewModel
    {
        /// <summary>
        /// Cart Item Id.
        /// </summary>
        [Required]
        public string Id { get; set; }

        /// <summary>
        /// Shopping Cart Id.
        /// </summary>
        [Required]
        public string ShoppingCartId { get; set; }

        /// <summary>
        /// Product Id.
        /// </summary>
        [Required]
        public string ProductId { get; set; }

        /// <summary>
        /// Product.
        /// </summary>
        public ProductViewModel Product { get; set; }

        /// <summary>
        /// Quantity.
        /// </summary>
        [Range(1, 1000, ErrorMessage = "Please enter a value between 1 and 1000")]
        public int Quantity { get; set; }

        /// <summary>
        /// Size Id.
        /// </summary>
        [Required]
        public string SizeId { get; set; }

        /// <summary>
        /// Size.
        /// </summary>
        [Required]
        public string Size { get; set; }

        /// <summary>
        /// Application User Id.
        /// </summary>
        [Required]
        public string ApplicationUserId { get; set; }

        /// <summary>
        /// Total.
        /// </summary>
        [Required]
        public string Total { get; set; }
    }
}