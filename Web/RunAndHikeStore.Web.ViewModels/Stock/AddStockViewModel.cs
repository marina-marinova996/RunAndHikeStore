namespace RunAndHikeStore.Web.ViewModels.Stock
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using RunAndHikeStore.Web.ViewModels.Size;

    public class AddStockViewModel
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
        /// ProductType.
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
        /// Size Id.
        /// </summary>
        [Required]
        public string SizeId { get; set; }

        /// <summary>
        /// Size.
        /// </summary>
        public string Size { get; set; }

        /// <summary>
        /// Sizes.
        /// </summary>
        public IEnumerable<SizeViewModel> Sizes { get; set; } = new List<SizeViewModel>();

        /// <summary>
        /// Units In stock.
        /// </summary>
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Units in Stock must be a positive number.")]
        public int UnitsInStock { get; set; }
    }
}
