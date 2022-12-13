namespace RunAndHikeStore.Web.ViewModels.Product
{
    using RunAndHikeStore.Web.ViewModels.Size;
    using System.Collections.Generic;

    public class ProductViewModel
    {
        /// <summary>
        /// Product Id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Product Number.
        /// </summary>
        public string ProductNumber { get; set; }

        /// <summary>
        /// Product Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Image URL.
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Color.
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Gender Id.
        /// </summary>
        public int GenderId { get; set; }

        /// <summary>
        /// Gender.
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Unit Price.
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Brand Id.
        /// </summary>
        public string BrandId { get; set; }

        /// <summary>
        /// Brand Name.
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// Product Type Id.
        /// </summary>
        public string ProductTypeId { get; set; }

        /// <summary>
        /// Product Type.
        /// </summary>
        public string ProductType { get; set; }

        /// <summary>
        /// Size Id.
        /// </summary>
        public string SizeId { get; set; }

        /// <summary>
        /// Size.
        /// </summary>
        public ProductSizeViewModel Size { get; set; }

        /// <summary>
        /// Sizes.
        /// </summary>
        public IEnumerable<ProductSizeViewModel> Sizes { get; set; } = new List<ProductSizeViewModel>();

        /// <summary>
        /// Categories.
        /// </summary>
        public IEnumerable<ProductCategoryViewModel> Categories { get; set; } = new List<ProductCategoryViewModel>();
    }
}
