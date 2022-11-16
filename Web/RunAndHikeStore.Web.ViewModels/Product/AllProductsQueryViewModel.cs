namespace RunAndHikeStore.Web.ViewModels.Product
{
    using RunAndHikeStore.Web.ViewModels.Product.Enum;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AllProductsQueryViewModel
    {
        public const int ProductsPerPage = 6;

        public int CurrentPage { get; set; } = 1;

        public ProductSorting Sorting { get; set; }

        public int TotalProductsCount { get; set; }

        public IEnumerable<string> MultiCategoriesIds { get; set; }

        public string ProductTypeId { get; set; }

        public string GenderId { get; set; }

        public IEnumerable<string> MultiBrandsIds { get; set; }

        public IEnumerable<string> MultiSizesIds { get; set; }

        public IEnumerable<ProductQueryViewModel> Products { get; set; } = new List<ProductQueryViewModel>();

        [Display(Name ="Search")]
        public string SearchTerm { get; set; }
    }
}