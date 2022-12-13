namespace RunAndHikeStore.Web.ViewModels.Product
{

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using RunAndHikeStore.Web.ViewModels.Product.Enum;

    public class ManageAllProductsViewModel
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
        /// Sorting.
        /// </summary>
        public ProductSorting Sorting { get; set; }

        /// <summary>
        /// Total Products count for pagination.
        /// </summary>
        public int TotalProductsCount { get; set; }

        /// <summary>
        /// Products
        /// </summary>
        public IEnumerable<ProductQueryManageAllViewModel> Products { get; set; } = new List<ProductQueryManageAllViewModel>();

        /// <summary>
        /// Search term.
        /// </summary>
        [Display(Name = "Search")]
        public string SearchTerm { get; set; }
    }
}
