namespace RunAndHikeStore.Web.ViewModels.Product
{
    using RunAndHikeStore.Web.ViewModels.Size;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ProductViewModel
    {
        public string Id { get; set; }

        public string ProductNumber { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string Color { get; set; }

        public int GenderId { get; set; }

        public string Gender { get; set; }

        public decimal UnitPrice { get; set; }

        public string Brand { get; set; }

        public string ProductType { get; set; }

        public string BrandId { get; set; }

        public string SizeId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Value for {0} must not be below {1}.")]
        public int Quantity { get; set; }

        public IEnumerable<ProductSizeViewModel> Sizes { get; set; } = new List<ProductSizeViewModel>();

        public string CategoryId { get; set; }

        public IEnumerable<ProductCategoryViewModel> Categories { get; set; } = new List<ProductCategoryViewModel>();

        public string ProductTypeId { get; set; }
    }
}
