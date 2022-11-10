using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RunAndHikeStore.Web.ViewModels.Product;

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