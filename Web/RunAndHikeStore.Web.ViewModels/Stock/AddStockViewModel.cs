using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RunAndHikeStore.Web.ViewModels.Size;

namespace RunAndHikeStore.Web.ViewModels.Stock
{
    public class AddStockViewModel
    {
        public string ProductId { get; set; }

        public string ProductNumber { get; set; }

        public string ProductType { get; set; }

        public string Brand { get; set; }

        public string Name { get; set; }

        public string Gender { get; set; }

        public string Color { get; set; }

        public decimal UnitPrice { get; set; }

        public string Description { get; set; }

        [Required]
        public string SizeId { get; set; }

        public string Size { get; set; }

        public IEnumerable<SizeViewModel> Sizes { get; set; } = new List<SizeViewModel>();

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Units in Stock must be a positive number.")]
        public int UnitsInStock { get; set; }
    }
}
