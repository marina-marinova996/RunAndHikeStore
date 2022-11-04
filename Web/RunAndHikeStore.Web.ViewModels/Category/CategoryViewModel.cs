namespace RunAndHikeStore.Web.ViewModels.Category
{
    using RunAndHikeStore.Web.ViewModels.Product;
    using System.Collections.Generic;

    public class CategoryViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}
