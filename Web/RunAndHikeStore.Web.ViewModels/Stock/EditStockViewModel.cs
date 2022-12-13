using RunAndHikeStore.Web.ViewModels.Size;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RunAndHikeStore.Web.ViewModels.Stock
{
    public class EditStockViewModel
    {
        /// <summary>
        /// Product Id.
        /// </summary>
        public string ProductId { get; set; }

        /// <summary>
        /// Product Number.
        /// </summary>
        public string ProductNumber { get; set; }

        /// <summary>
        /// Product Type.
        /// </summary>
        public string ProductType { get; set; }

        /// <summary>
        /// Brand.
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// Product Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gender.
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Color.
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Unit Price.
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Units In Stock.
        /// </summary>
        [Required]
        public int UnitsInStock { get; set; }

        /// <summary>
        /// Size Id.
        /// </summary>
        [Required]
        public string SizeId { get; set; }

        /// <summary>
        /// Size name.
        /// </summary>
        public string SizeName { get; set; }

        /// <summary>
        /// Sizes.
        /// </summary>
        public IEnumerable<SizeViewModel> Sizes { get; set; } = new List<SizeViewModel>();
    }
}
