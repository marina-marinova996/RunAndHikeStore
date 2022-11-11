namespace RunAndHikeStore.Web.ViewModels.Product
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using RunAndHikeStore.Web.ViewModels.Brand;
    using RunAndHikeStore.Web.ViewModels.Category;
    using RunAndHikeStore.Web.ViewModels.Product.Enum;
    using RunAndHikeStore.Web.ViewModels.Size;

    public class AllProductsViewModel
    {
        public const int ProductsPerPage = 6;

        public int CurrentPage { get; set; } = 1;

        public ProductSorting Sorting { get; set; }

        public int TotalProductsCount { get; set; }

        public IEnumerable<string> MultiCategoriesIds { get; set; }

        public IEnumerable<string> MultiProductTypesIds { get; set; }

        public IEnumerable<string> MultiGendersIds { get; set; }

        public IEnumerable<string> MultiBrandsIds { get; set; }

        public IEnumerable<string> MultiSizesIds { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; } = new List<ProductViewModel>();

        [Display(Name ="Search")]
        public string SearchTerm { get; set; }
    }
}