using RunAndHikeStore.Web.ViewModels.Product.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace RunAndHikeStore.Web.ViewModels.Product
{
    public class ManageAllProductsViewModel
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

        public IEnumerable<ProductQueryManageAllViewModel> Products { get; set; } = new List<ProductQueryManageAllViewModel>();

        [Display(Name = "Search")]
        public string SearchTerm { get; set; }
    }
}
