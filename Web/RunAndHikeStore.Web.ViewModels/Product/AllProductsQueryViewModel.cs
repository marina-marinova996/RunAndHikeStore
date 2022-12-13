namespace RunAndHikeStore.Web.ViewModels.Product
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using RunAndHikeStore.Web.ViewModels.Product.Enum;

    public class AllProductsQueryViewModel
    {
        /// <summary>
        /// Products per page.
        /// </summary>
        public const int ProductsPerPage = 6;

        /// <summary>
        /// Current page.
        /// </summary>
        public int CurrentPage { get; set; } = 1;

        /// <summary>
        /// Sorting
        /// </summary>
        public ProductSorting Sorting { get; set; }

        /// <summary>
        /// Total Products Count.
        /// </summary>
        public int TotalProductsCount { get; set; }

        /// <summary>
        /// Multi Categories Ids - used for Multiple Select.
        /// </summary>
        public IEnumerable<string> MultiCategoriesIds { get; set; }

        /// <summary>
        /// Product Type Id.
        /// </summary>
        public string ProductTypeId { get; set; }

        /// <summary>
        /// Gender Id.
        /// </summary>
        public string GenderId { get; set; }

        /// <summary>
        /// Multi Brands Ids used for Multiple select.
        /// </summary>
        public IEnumerable<string> MultiBrandsIds { get; set; }

        /// <summary>
        /// Multi Sizes Ids used for Multiple select.
        /// </summary>
        public IEnumerable<string> MultiSizesIds { get; set; }

        /// <summary>
        /// Products.
        /// </summary>
        public IEnumerable<ProductQueryViewModel> Products { get; set; } = new List<ProductQueryViewModel>();

        /// <summary>
        /// Search term.
        /// </summary>
        [Display(Name ="Search")]
        public string SearchTerm { get; set; }
    }
}