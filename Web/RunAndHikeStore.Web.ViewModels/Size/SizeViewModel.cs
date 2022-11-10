using RunAndHikeStore.Web.ViewModels.Product;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RunAndHikeStore.Web.ViewModels.Size
{
    public class SizeViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Product Type")]
        public string ProductType { get; set; }

        public string ProductTypeId { get; set; }

        public IEnumerable<ProductTypeViewModel> ProductTypes { get; set; } = new List<ProductTypeViewModel>();
    }
}