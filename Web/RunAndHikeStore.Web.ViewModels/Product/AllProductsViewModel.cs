namespace RunAndHikeStore.Web.ViewModels.Product
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using RunAndHikeStore.Web.ViewModels.Brand;
    using RunAndHikeStore.Web.ViewModels.Category;
    using RunAndHikeStore.Web.ViewModels.Size;

    public class AllProductsViewModel
    {
        public IEnumerable<ProductTypeViewModel> ProductTypes { get; set; } = new List<ProductTypeViewModel>();

        public IEnumerable<GenderViewModel> Genders { get; set; } = new List<GenderViewModel>();

        public IEnumerable<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();

        public IEnumerable<BrandViewModel> Brands { get; set; } = new List<BrandViewModel>();

        public IEnumerable<SizeViewModel> Sizes { get; set; } = new List<SizeViewModel>();

        public IEnumerable<ProductViewModel> Products { get; set; } = new List<ProductViewModel>();

        [Display(Name ="Search")]
        public string SearchTerm { get; set; }
    }
}