using RunAndHikeStore.Web.ViewModels.Product;
using System;
using System.ComponentModel.DataAnnotations;

namespace RunAndHikeStore.Web.ViewModels.ShoppingCart
{
    public class CartItemViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string ShoppingCartId { get; set; }

        [Required]
        public string ProductId { get; set; }

        public ProductViewModel Product { get; set; }

        [Range(1, 1000, ErrorMessage = "Please enter a value between 1 and 1000")]
        public int Quantity { get; set; }

        [Required]
        public string SizeId { get; set; }

        [Required]
        public string Size { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        [Required]
        public string Total { get; set; }
    }
}